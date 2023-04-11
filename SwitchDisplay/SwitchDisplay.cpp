#include "SwitchDisplay.h"
#include <windows.h>
#include <iostream>
#include <string>
#include <vector>
#include <wchar.h>


void QueryDisplay(unsigned int index, wchar_t* str_out)
{
	std::vector<DISPLAYCONFIG_PATH_INFO> paths;
	std::vector<DISPLAYCONFIG_MODE_INFO> modes;
	UINT32 flags = QDC_ONLY_ACTIVE_PATHS;
	LONG isError = ERROR_INSUFFICIENT_BUFFER;

	UINT32 pathCount, modeCount;
	isError = GetDisplayConfigBufferSizes(flags, &pathCount, &modeCount);

	if (isError)
	{
		return;
	}

	// Allocate the path and mode arrays
	paths.resize(pathCount);
	modes.resize(modeCount);

	// Get all active paths and their modes
	isError = QueryDisplayConfig(flags, &pathCount, paths.data(), &modeCount, modes.data(), nullptr);

	// The function may have returned fewer paths/modes than estimated
	paths.resize(pathCount);
	modes.resize(modeCount);


	if (isError) return;

	std::wstring mon_name = L"";

	// For each active path
	int len = paths.size();
	if (index >= len) return;

	// Find the target (monitor) friendly name
	DISPLAYCONFIG_TARGET_DEVICE_NAME targetName = {};
	targetName.header.adapterId = paths[index].targetInfo.adapterId;
	targetName.header.id = paths[index].targetInfo.id;
	targetName.header.type = DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME;
	targetName.header.size = sizeof(targetName);
	isError = DisplayConfigGetDeviceInfo(&targetName.header);

	if (isError) return;

	if (targetName.flags.friendlyNameFromEdid)
	{
		mon_name += targetName.monitorFriendlyDeviceName;
	}
	else
	{
		mon_name = L"Unknown";
	}

	wcscpy_s(str_out, wcslen(mon_name.c_str()) + 1, mon_name.c_str());
	//covert wstring to wchar 	

}

void SetMonitorPrimary(unsigned int index)
{
	LONG second_x = 0;
	LONG second_y = 0;

	DWORD deviceNum = index;
	DISPLAY_DEVICE displayDevice;
	DEVMODE devMode;

	devMode.dmSize = sizeof(DEVMODE);
	displayDevice.cb = sizeof(DISPLAY_DEVICE);

	EnumDisplayDevices(NULL, deviceNum, &displayDevice, 0);
	EnumDisplaySettings(displayDevice.DeviceName, ENUM_CURRENT_SETTINGS, &devMode);

	second_x = devMode.dmPosition.x;
	second_y = devMode.dmPosition.y;
	devMode.dmPosition.x = 0;
	devMode.dmPosition.y = 0;

	DWORD dwFlags = CDS_UPDATEREGISTRY | CDS_NORESET | CDS_SET_PRIMARY;
	ChangeDisplaySettingsEx((LPCWSTR)displayDevice.DeviceName, &devMode, NULL, dwFlags, NULL);

	displayDevice.cb = sizeof(DISPLAY_DEVICE);

	for (unsigned int otherid = 0; EnumDisplayDevices(NULL, otherid, &displayDevice, 0); otherid++)
	{
		if ((displayDevice.StateFlags & DISPLAY_DEVICE_ATTACHED_TO_DESKTOP) && otherid != deviceNum)
		{
			displayDevice.cb = sizeof(DISPLAY_DEVICE);
			DEVMODE otherDeviceMode;
			otherDeviceMode.dmSize = sizeof(DEVMODE);
			EnumDisplaySettings(displayDevice.DeviceName, ENUM_CURRENT_SETTINGS, &otherDeviceMode);

			otherDeviceMode.dmPosition.x -= second_x;
			otherDeviceMode.dmPosition.y -= second_y;

			dwFlags = CDS_UPDATEREGISTRY | CDS_NORESET;
			ChangeDisplaySettingsEx((LPCWSTR)displayDevice.DeviceName, &otherDeviceMode, NULL, dwFlags, NULL);
		}

		displayDevice.cb = sizeof(DISPLAY_DEVICE);
	}

	// Apply settings
	ChangeDisplaySettingsEx(NULL, NULL, NULL, 0, NULL);

}

unsigned int GetMonitorPrimary()
{
	DISPLAY_DEVICE displayDevice;
	DEVMODE devMode;

	devMode.dmSize = sizeof(DEVMODE);
	displayDevice.cb = sizeof(DISPLAY_DEVICE);

	for (unsigned int i = 0; EnumDisplayDevices(NULL, i, &displayDevice, 0); i++)
	{
		EnumDisplaySettings(displayDevice.DeviceName, ENUM_CURRENT_SETTINGS, &devMode);
		if (devMode.dmPosition.x == 0 && devMode.dmPosition.y == 0)
		{
			//return primary monitor
			return i;
		}
	}

}

unsigned int MonitorCount()
{
	std::vector<DISPLAYCONFIG_PATH_INFO> paths;
	std::vector<DISPLAYCONFIG_MODE_INFO> modes;
	UINT32 flags = QDC_ONLY_ACTIVE_PATHS;
	LONG isError = ERROR_INSUFFICIENT_BUFFER;

	UINT32 pathCount, modeCount;
	isError = GetDisplayConfigBufferSizes(flags, &pathCount, &modeCount);

	if (isError) return 0;

	// Allocate the path and mode arrays
	paths.resize(pathCount);
	modes.resize(modeCount);

	// Get all active paths and their modes
	isError = QueryDisplayConfig(flags, &pathCount, paths.data(), &modeCount, modes.data(), nullptr);

	// The function may have returned fewer paths/modes than estimated
	paths.resize(pathCount);
	modes.resize(modeCount);

	if (isError) return 0;
	
	return paths.size();
}



