START ../../PacketGenerator/bin/debug/PacketGenerator.exe ../../PacketGenerator/PDL.xml
XCOPY /Y GenPackets.cs "../../DummyClient/Packet"
XCOPY /Y GenPackets.cs "../../MainServer/Packet"
XCOPY /Y GenPackets.cs "../../../NetProject/Assets/Scripts/Packet"
XCOPY /Y ClientPacketManager.cs "../../DummyClient/Packet"
XCOPY /Y ClientPacketManager.cs "../../../NetProject/Assets/Scripts/Packet"
XCOPY /Y ServerPacketManager.cs "../../MainServer/Packet" 





  