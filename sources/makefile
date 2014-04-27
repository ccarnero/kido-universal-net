FSC=/Library/Frameworks/Mono.framework/Versions/3.2.6/lib/mono/4.0/fsc.exe
FILES=./kidozen.core/AsyncStreamReader.fs ./kidozen.core/HttpClient.fs ./kidozen.core/types.fs ./kidozen.core/Core.fs
compiler: compiler.exe

compiler.exe: $(FSFILES)
	$(FSC) --define:LINUX --debug:full --debug+ $(FSFILES) -o:compiler.exe --sig:sigfile.fs

run: compiler
	mono compiler.exe