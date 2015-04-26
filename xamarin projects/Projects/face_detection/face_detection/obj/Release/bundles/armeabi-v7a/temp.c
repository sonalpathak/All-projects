/* This source code was produced by mkbundle, do not edit */

#ifndef NULL
#define NULL (void *)0
#endif

typedef struct {
	const char *name;
	const unsigned char *data;
	const unsigned int size;
} MonoBundledAssembly;
void          mono_register_bundled_assemblies (const MonoBundledAssembly **assemblies);
void          register_config_for_assembly_func (const char* assembly_name, const char* config_xml);

extern const unsigned char assembly_data_face_detection_dll [];
static const MonoBundledAssembly assembly_bundle_face_detection_dll = {"face_detection.dll", assembly_data_face_detection_dll, 6144};
extern const unsigned char assembly_data_Mono_Android_dll [];
static const MonoBundledAssembly assembly_bundle_Mono_Android_dll = {"Mono.Android.dll", assembly_data_Mono_Android_dll, 560128};
extern const unsigned char assembly_data_System_dll [];
static const MonoBundledAssembly assembly_bundle_System_dll = {"System.dll", assembly_data_System_dll, 262144};
extern const unsigned char assembly_data_Mono_Security_dll [];
static const MonoBundledAssembly assembly_bundle_Mono_Security_dll = {"Mono.Security.dll", assembly_data_Mono_Security_dll, 154624};
extern const unsigned char assembly_data_System_Core_dll [];
static const MonoBundledAssembly assembly_bundle_System_Core_dll = {"System.Core.dll", assembly_data_System_Core_dll, 32256};
extern const unsigned char assembly_data_mscorlib_dll [];
static const MonoBundledAssembly assembly_bundle_mscorlib_dll = {"mscorlib.dll", assembly_data_mscorlib_dll, 1314816};

static const MonoBundledAssembly *bundled [] = {
	&assembly_bundle_face_detection_dll,
	&assembly_bundle_Mono_Android_dll,
	&assembly_bundle_System_dll,
	&assembly_bundle_Mono_Security_dll,
	&assembly_bundle_System_Core_dll,
	&assembly_bundle_mscorlib_dll,
	NULL
};

static char *image_name = "face_detection.dll";

static void install_dll_config_files (void (register_config_for_assembly_func)(const char *, const char *)) {

}

static const char *config_dir = NULL;
void mono_mkbundle_init (void (register_bundled_assemblies_func)(const MonoBundledAssembly **), void (register_config_for_assembly_func)(const char *, const char *))
{
	install_dll_config_files (register_config_for_assembly_func);
	register_bundled_assemblies_func(bundled);
}
