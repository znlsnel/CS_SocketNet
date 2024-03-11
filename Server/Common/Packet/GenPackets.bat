START ../../PacketGenerator/bin/debug/PacketGenerator.exe ../../PacketGenerator/PDL.xml
XCOPY /Y GenPackets.cs "../../DummyClient/Packet"
XCOPY /Y GenPackets.cs "../../MainServer/Packet"
XCOPY /Y GenPackets.cs "D:\PROJECTS\Unity Projects\NetProject\Assets\Scripts\Packet"
XCOPY /Y ClientPacketManager.cs "../../DummyClient/Packet"
XCOPY /Y ServerPacketManager.cs "../../MainServer/Packet" 
XCOPY /Y ServerPacketManager.cs "D:\PROJECTS\Unity Projects\NetProject\Assets\Scripts\Packet" 





  