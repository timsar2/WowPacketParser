using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class InstanceHandler
    {

        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_END)]
        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_PHASE_SHIFT_CHANGED)]
        public static void HandleInstanceZero(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_SAVE_CUF_PROFILES)]
        [Parser(Opcode.SMSG_LOAD_CUF_PROFILES)]
        public static void HandleCUFProfiles(Packet packet)
        {
            var count = packet.ReadUInt32("Count");

            for (int i = 0; i < count; ++i)
            {
                var strlen = packet.ReadBits(7);

                packet.ReadBit("KeepGroupsTogether", i);
                packet.ReadBit("DisplayPets", i);
                packet.ReadBit("DisplayMainTankAndAssist", i);
                packet.ReadBit("DisplayHealPrediction", i);
                packet.ReadBit("DisplayAggroHighlight", i);
                packet.ReadBit("DisplayOnlyDispellableDebuffs", i);
                packet.ReadBit("DisplayPowerBar", i);
                packet.ReadBit("DisplayBorder", i);
                packet.ReadBit("UseClassColors", i);
                packet.ReadBit("HorizontalGroups", i);
                packet.ReadBit("DisplayNonBossDebuffs", i);
                packet.ReadBit("DynamicPosition", i);
                packet.ReadBit("Locked", i);
                packet.ReadBit("Shown", i);
                packet.ReadBit("AutoActivate2Players", i);
                packet.ReadBit("AutoActivate3Players", i);
                packet.ReadBit("AutoActivate5Players", i);
                packet.ReadBit("AutoActivate10Players", i);
                packet.ReadBit("AutoActivate15Players", i);
                packet.ReadBit("AutoActivate25Players", i);
                packet.ReadBit("AutoActivate40Players", i);
                packet.ReadBit("AutoActivateSpec1", i);
                packet.ReadBit("AutoActivateSpec2", i);
                packet.ReadBit("AutoActivatePvP", i);
                packet.ReadBit("AutoActivatePvE", i);

                packet.ReadInt16("FrameHeight", i);
                packet.ReadInt16("FrameWidth", i);

                packet.ReadByte("SortBy", i);
                packet.ReadByte("HealthText", i);
                packet.ReadByte("TopPoint", i);
                packet.ReadByte("BottomPoint", i);
                packet.ReadByte("LeftPoint", i);

                packet.ReadInt16("TopOffset", i);
                packet.ReadInt16("BottomOffset", i);
                packet.ReadInt16("LeftOffset", i);

                packet.ReadWoWString("Name", strlen, i);
            }
        }

        [Parser(Opcode.CMSG_SET_DUNGEON_DIFFICULTY)]
        public static void HandleSetDungeonDifficulty(Packet packet)
        {
            packet.ReadInt32("DifficultyID");
        }

        [Parser(Opcode.CMSG_SET_RAID_DIFFICULTY)]
        public static void HandleSetRaidDifficulty(Packet packet)
        {
            packet.ReadInt32("DifficultyID");
            packet.ReadByte("Force");
        }

        [Parser(Opcode.SMSG_INSTANCE_INFO)]
        public static void HandleInstanceInfo(Packet packet)
        {
            var int16 = packet.ReadInt32("LocksCount");
            for (int i = 0; i < int16; i++)
            {
                packet.ReadInt32("MapID", i);
                packet.ReadInt32("DifficultyID", i);
                packet.ReadInt64("InstanceID", i);
                packet.ReadInt32("TimeRemaining", i);
                packet.ReadInt32("Completed_mask", i);

                packet.ResetBitReader();
                packet.ReadBit("Locked", i);
                packet.ReadBit("Extended", i);
            }
        }

        [Parser(Opcode.CMSG_SET_SAVED_INSTANCE_EXTEND)]
        public static void HandleSetSavedInstanceExtend(Packet packet)
        {
            packet.ReadEntry<Int32>(StoreNameType.Map, "MapID");
            packet.ReadInt32("DifficultyID");
            packet.ReadBit("Extended");
        }

        [Parser(Opcode.SMSG_RAID_INSTANCE_MESSAGE)]
        public static void HandleRaidInstanceMessage(Packet packet)
        {
            packet.ReadByte("Type");

            packet.ReadInt32("MapID");
            packet.ReadInt32("DifficultyID");
            packet.ReadInt32("TimeLeft");

            packet.ReadBit("Locked");
            packet.ReadBit("Extended");
        }

        [Parser(Opcode.SMSG_INSTANCE_SAVE_CREATED)]
        public static void HandleInstanceSaveCreated(Packet packet)
        {
            packet.ReadBit("Gm");
        }

        [Parser(Opcode.SMSG_CHANGE_PLAYER_DIFFICULTY_RESULT)]
        public static void HandlePlayerChangeDifficulty(Packet packet)
        {
            var type = packet.ReadBits("Result", 4);
            switch (type)
            {
                case 5:
                case 8:
                    packet.ReadBit("Cooldown");
                    packet.ReadUInt32("CooldownReason");
                    break;
                case 11:
                    packet.ReadUInt32("InstanceDifficultyID");
                    packet.ReadUInt32("DifficultyRecID");
                    break;
                case 2:
                    packet.ReadUInt32("MapID");
                    break;
                case 4:
                    packet.ReadPackedGuid128("Guid");
                    break;
            }
        }
    }
}
