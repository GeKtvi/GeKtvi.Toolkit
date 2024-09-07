SET ERRORLEVEL=1
"%programfiles(x86)%\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8 Tools\SvcUtil.exe" net.pipe://localhost/WcfUnitTest /n:*,GeKtvi.Toolkit.Wcf.Tests /out:GeneratedClient.cs /noConfig
pause
EXIT 0