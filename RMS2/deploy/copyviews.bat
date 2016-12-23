@echo off
set dest=E:\RMS2Views

echo CREATE DESTINATION FOLDER
echo.
md %dest%

echo COPY FILES
echo.

xcopy ..\src\rmsweb\rmsweb\Areas\CMSI01\Views\* %dest%\CMSI01\Views\* /Y /E /F

xcopy ..\src\rmsweb\rmsweb\Areas\CMSI02\Views\* %dest%\CMSI02\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\CMSI02\advanced-search.json %dest%\CMSI02\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\CMSI03\Views\* %dest%\CMSI03\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\CMSI03\advanced-search.json %dest%\CMSI03\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\CMSI04\Views\* %dest%\CMSI04\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\CMSI04\advanced-search.json %dest%\CMSI04\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\CMSI05\Views\* %dest%\CMSI05\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\CMSI05\advanced-search.json %dest%\CMSI05\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\CMSI06\Views\* %dest%\CMSI06\Views\* /Y /E /F

xcopy ..\src\rmsweb\rmsweb\Areas\CMSI07\Views\* %dest%\CMSI07\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\CMSI07\advanced-search.json %dest%\CMSI07\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\CMSI08\Views\* %dest%\CMSI08\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\CMSI08\advanced-search.json %dest%\CMSI08\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\CMSI09\Views\* %dest%\CMSI09\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\CMSI09\advanced-search.json %dest%\CMSI09\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\CMSI10\Views\* %dest%\CMSI10\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\CMSI10\advanced-search.json %dest%\CMSI10\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\CMSI11\Views\* %dest%\CMSI11\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\CMSI11\advanced-search.json %dest%\CMSI11\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SUPI01\Views\* %dest%\SUPI01\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SUPI01\advanced-search.json %dest%\SUPI01\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SUPI03\Views\* %dest%\SUPI03\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SUPI03\advanced-search.json %dest%\SUPI03\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SUPI04\Views\* %dest%\SUPI04\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SUPI04\advanced-search.json %dest%\SUPI04\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SUPI05\Views\* %dest%\SUPI05\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SUPI05\advanced-search.json %dest%\SUPI05\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SUPI06\Views\* %dest%\SUPI06\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SUPI06\advanced-search.json %dest%\SUPI06\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SUPI07\Views\* %dest%\SUPI07\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SUPI07\advanced-search.json %dest%\SUPI07\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SUPI08\Views\* %dest%\SUPI08\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SUPI08\advanced-search.json %dest%\SUPI08\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SUPI09\Views\* %dest%\SUPI09\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SUPI09\advanced-search.json %dest%\SUPI09\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SUPB01\Views\* %dest%\SUPB01\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SUPB01\advanced-search.json %dest%\SUPB01\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SUPB02\Views\* %dest%\SUPB02\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SUPB02\advanced-search.json %dest%\SUPB02\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SUPB02\Views\* %dest%\SUPB03\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SUPB02\advanced-search.json %dest%\SUPB03\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\CONI01\Views\* %dest%\CONI01\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\CONI01\advanced-search.json %dest%\CONI01\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\CONI02\Views\* %dest%\CONI02\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\CONI02\advanced-search.json %dest%\CONI02\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\CONI03\Views\* %dest%\CONI03\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\CONI03\advanced-search.json %dest%\CONI03\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\PSMI01\Views\* %dest%\PSMI01\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\PSMI01\advanced-search.json %dest%\PSMI01\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\PSMI02\Views\* %dest%\PSMI02\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\PSMI02\advanced-search.json %dest%\PSMI02\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\PSMI03\Views\* %dest%\PSMI03\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\PSMI03\advanced-search.json %dest%\PSMI03\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\BOMI01\Views\* %dest%\BOMI01\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\BOMI01\advanced-search.json %dest%\BOMI01\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\BOMI02\Views\* %dest%\BOMI02\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\BOMI02\advanced-search.json %dest%\BOMI02\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SERI01\Views\* %dest%\SERI01\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SERI01\advanced-search.json %dest%\SERI01\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SERI02\Views\* %dest%\SERI02\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SERI02\advanced-search.json %dest%\SERI02\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SERI03\Views\* %dest%\SERI03\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SERI03\advanced-search.json %dest%\SERI03\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SERI04\Views\* %dest%\SERI04\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SERI04\advanced-search.json %dest%\SERI04\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SERI05\Views\* %dest%\SERI05\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SERI05\advanced-search.json %dest%\SERI05\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SERI06\Views\* %dest%\SERI06\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SERI06\advanced-search.json %dest%\SERI06\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SERI07\Views\* %dest%\SERI07\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SERI07\advanced-search.json %dest%\SERI07\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SERI08\Views\* %dest%\SERI08\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SERI08\advanced-search.json %dest%\SERI08\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SERI09\Views\* %dest%\SERI09\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SERI09\advanced-search.json %dest%\SERI09\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SERI10\Views\* %dest%\SERI10\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SERI10\advanced-search.json %dest%\SERI10\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SERI11\Views\* %dest%\SERI11\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SERI11\advanced-search.json %dest%\SERI11\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SERI12\Views\* %dest%\SERI12\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SERI12\advanced-search.json %dest%\SERI12\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SERI13\Views\* %dest%\SERI13\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SERI13\advanced-search.json %dest%\SERI13\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SERB01\Views\* %dest%\SERB01\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SERB01\advanced-search.json %dest%\SERB01\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SERB02\Views\* %dest%\SERB02\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SERB02\advanced-search.json %dest%\SERB02\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SERB03\Views\* %dest%\SERB03\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SERB03\advanced-search.json %dest%\SERB03\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SERR01\Views\* %dest%\SERR01\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SERR01\advanced-search.json %dest%\SERR01\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\SERQ01\Views\* %dest%\SERQ01\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\SERQ01\advanced-search.json %dest%\SERQ01\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\ADMI01\Views\* %dest%\ADMI01\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\ADMI01\advanced-search.json %dest%\ADMI01\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\ADMI02\Views\* %dest%\ADMI02\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\ADMI02\advanced-search.json %dest%\ADMI02\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\ADMI03\Views\* %dest%\ADMI03\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\ADMI03\advanced-search.json %dest%\ADMI03\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\ADMI04\Views\* %dest%\ADMI04\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\ADMI04\advanced-search.json %dest%\ADMI04\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\ADMI05\Views\* %dest%\ADMI05\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\ADMI05\advanced-search.json %dest%\ADMI05\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\RMAI01\Views\* %dest%\RMAI01\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\RMAI01\advanced-search.json %dest%\RMAI01\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\RMAI02\Views\* %dest%\RMAI02\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\RMAI02\advanced-search.json %dest%\RMAI02\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\RMAI03\Views\* %dest%\RMAI03\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\RMAI03\advanced-search.json %dest%\RMAI03\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\RMAI04\Views\* %dest%\RMAI04\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\RMAI04\advanced-search.json %dest%\RMAI04\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\RMAI05\Views\* %dest%\RMAI05\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\RMAI05\advanced-search.json %dest%\RMAI05\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\RMAI06\Views\* %dest%\RMAI06\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\RMAI06\advanced-search.json %dest%\RMAI06\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\RMAI07\Views\* %dest%\RMAI07\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\RMAI07\advanced-search.json %dest%\RMAI07\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\RMAB01\Views\* %dest%\RMAB01\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\RMAB01\advanced-search.json %dest%\RMAB01\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\RMAB02\Views\* %dest%\RMAB02\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\RMAB02\advanced-search.json %dest%\RMAB02\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\RMAB03\Views\* %dest%\RMAB03\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\RMAB03\advanced-search.json %dest%\RMAB03\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\RMAB04\Views\* %dest%\RMAB04\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\RMAB04\advanced-search.json %dest%\RMAB04\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\RMAB05\Views\* %dest%\RMAB05\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\RMAB05\advanced-search.json %dest%\RMAB05\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\RMAQ01\Views\* %dest%\RMAQ01\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\RMAQ01\advanced-search.json %dest%\RMAQ01\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\PMAI01\Views\* %dest%\PMAI01\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\PMAI01\advanced-search.json %dest%\PMAI01\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\PMAI02\Views\* %dest%\PMAI02\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\PMAI02\advanced-search.json %dest%\PMAI02\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\PMAI03\Views\* %dest%\PMAI03\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\PMAI03\advanced-search.json %dest%\PMAI03\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\PMAI04\Views\* %dest%\PMAI04\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\PMAI04\advanced-search.json %dest%\PMAI04\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\PMAI05\Views\* %dest%\PMAI05\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\PMAI05\advanced-search.json %dest%\PMAI05\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\PMAI06\Views\* %dest%\PMAI06\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\PMAI06\advanced-search.json %dest%\PMAI06\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\PMAB01\Views\* %dest%\PMAB01\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\PMAB01\advanced-search.json %dest%\PMAB01\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\PMAB02\Views\* %dest%\PMAB02\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\PMAB02\advanced-search.json %dest%\PMAB02\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\PMAB03\Views\* %dest%\PMAB03\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\PMAB03\advanced-search.json %dest%\PMAB03\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\PMAB04\Views\* %dest%\PMAB04\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\PMAB04\advanced-search.json %dest%\PMAB04\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\REPQ01\Views\* %dest%\REPQ01\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\REPQ01\advanced-search.json %dest%\REPQ01\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\REPQ02\Views\* %dest%\REPQ02\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\REPQ02\advanced-search.json %dest%\REPQ02\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\REPQ03\Views\* %dest%\REPQ03\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\REPQ03\advanced-search.json %dest%\REPQ03\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\REPQ04\Views\* %dest%\REPQ04\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\REPQ04\advanced-search.json %dest%\REPQ04\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\REPQ05\Views\* %dest%\REPQ05\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\REPQ05\advanced-search.json %dest%\REPQ05\advanced-search.json /Y

xcopy ..\src\rmsweb\rmsweb\Areas\REPQ06\Views\* %dest%\REPQ06\Views\* /Y /E /F
copy ..\src\rmsweb\rmsweb\Areas\REPQ06\advanced-search.json %dest%\REPQ06\advanced-search.json /Y

echo.
echo OPERATION COMPLETED
pause
