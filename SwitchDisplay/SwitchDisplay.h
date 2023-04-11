



using namespace std;

#ifdef __cplusplus
extern "C" {
#endif

#ifdef SWITCHDISPLAY_EXPORTS 
#define DLL_API __declspec(dllexport)
#else
#define DLL_API __declspec(dllimport)
#endif
	DLL_API void  QueryDisplay(unsigned int, wchar_t*);
	DLL_API void  SetMonitorPrimary(unsigned int index);
	DLL_API unsigned int  GetMonitorPrimary();
	DLL_API unsigned int  MonitorCount();

#ifdef __cplusplus
}
#endif

