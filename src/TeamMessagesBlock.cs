using Microsoft.Extensions.Configuration;
using Sharp.Shared;
using Sharp.Shared.Enums;
using Sharp.Shared.HookParams;
using Sharp.Shared.Listeners;
using Sharp.Shared.Managers;
using Sharp.Shared.Objects;
using Sharp.Shared.Types;
using Sharp.Shared.Units;

namespace MS_TeamMessagesBlock
{
    public class TeamMessagesBlock : IModSharpModule, IClientListener
    {
        public string DisplayName => "TeamMessagesBlock";
        public string DisplayAuthor => "DarkerZ[RUS]";
        public TeamMessagesBlock(ISharedSystem sharedSystem, string dllPath, string sharpPath, Version version, IConfiguration coreConfiguration, bool hotReload)
        {
            _clients = sharedSystem.GetClientManager();
            _hooks = sharedSystem.GetHookManager();
        }
        private readonly IClientManager _clients;
        private readonly IHookManager _hooks;

        public bool Init()
        {
            foreach (var sCommand in BlockCommandArray) _clients.InstallCommandListener(sCommand, OnClientCommand);

            _hooks.TextMsg.InstallHookPre(OnTextMessage);
            return true;
        }

        public void Shutdown()
        {
            foreach (var sCommand in BlockCommandArray) _clients.RemoveCommandListener(sCommand, OnClientCommand);

            _hooks.TextMsg.RemoveHookPre(OnTextMessage);
        }

        private ECommandAction OnClientCommand(IGameClient client, StringCommand command)
        {
            return ECommandAction.Handled;
        }

        private HookReturnValue<NetworkReceiver> OnTextMessage(ITextMsgHookParams param, HookReturnValue<NetworkReceiver> value)
        {
            for (int i = 0; i < TeamWarningArray.Length; i++)
                if (param.Name.Contains(TeamWarningArray[i])) return new HookReturnValue<NetworkReceiver>(EHookAction.SkipCallReturnOverride);


            for (int i = 0; i < MoneyMessageArray.Length; i++)
                if (param.Name.Contains(MoneyMessageArray[i])) return new HookReturnValue<NetworkReceiver>(EHookAction.SkipCallReturnOverride);

            for (int i = 0; i < SavedbyArray.Length; i++)
                if (param.Name.Contains(SavedbyArray[i])) return new HookReturnValue<NetworkReceiver>(EHookAction.SkipCallReturnOverride);

            if (param.Name.Contains("Pet_Killed")) return new HookReturnValue<NetworkReceiver>(EHookAction.SkipCallReturnOverride);

            return default;
        }

        private static readonly string[] BlockCommandArray = [
            "coverme",
            "takepoint",
            "holdpos",
            "regroup",
            "followme",
            "takingfire",
            "go",
            "fallback",
            "sticktog",
            "getinpos",
            "stormfront",
            "report",
            "roger",
            "enemyspot",
            "needbackup",
            "sectorclear",
            "inposition",
            "reportingin",
            "getout",
            "negative",
            "enemydown",
            "sorry",
            "cheer",
            "compliment",
            "thanks",
            "go_a",
            "go_b",
            "needrop",
            "deathcry",
            "playerchatwheel"
        ];
        private static readonly string[] MoneyMessageArray = [
            "Player_Cash_Award_Kill_Teammate",
            "Player_Cash_Award_Killed_VIP",
            "Player_Cash_Award_Killed_Enemy_Generic",
            "Player_Cash_Award_Killed_Enemy",
            "Player_Cash_Award_Bomb_Planted",
            "Player_Cash_Award_Bomb_Defused",
            "Player_Cash_Award_Rescued_Hostage",
            "Player_Cash_Award_Interact_Hostage",
            "Player_Cash_Award_Respawn",
            "Player_Cash_Award_Get_Killed",
            "Player_Cash_Award_Damage_Hostage",
            "Player_Cash_Award_Kill_Hostage",
            "Player_Point_Award_Killed_Enemy",
            "Player_Point_Award_Killed_Enemy_Plural",
            "Player_Point_Award_Killed_Enemy_NoWeapon",
            "Player_Point_Award_Killed_Enemy_NoWeapon_Plural",
            "Player_Point_Award_Assist_Enemy",
            "Player_Point_Award_Assist_Enemy_Plural",
            "Player_Point_Award_Picked_Up_Dogtag",
            "Player_Point_Award_Picked_Up_Dogtag_Plural",
            "Player_Team_Award_Killed_Enemy",
            "Player_Team_Award_Killed_Enemy_Plural",
            "Player_Team_Award_Bonus_Weapon",
            "Player_Team_Award_Bonus_Weapon_Plural",
            "Player_Team_Award_Picked_Up_Dogtag",
            "Player_Team_Award_Picked_Up_Dogtag_Plural",
            "Player_Team_Award_Picked_Up_Dogtag_Friendly",
            "Player_Cash_Award_ExplainSuicide_YouGotCash",
            "Player_Cash_Award_ExplainSuicide_TeammateGotCash",
            "Player_Cash_Award_ExplainSuicide_EnemyGotCash",
            "Player_Cash_Award_ExplainSuicide_Spectators",
            "Team_Cash_Award_T_Win_Bomb",
            "Team_Cash_Award_Elim_Hostage",
            "Team_Cash_Award_Elim_Bomb",
            "Team_Cash_Award_Win_Time",
            "Team_Cash_Award_Win_Defuse_Bomb",
            "Team_Cash_Award_Win_Hostages_Rescue",
            "Team_Cash_Award_Win_Hostage_Rescue",
            "Team_Cash_Award_Loser_Bonus",
            "Team_Cash_Award_Bonus_Shorthanded",
            "Notice_Bonus_Enemy_Team",
            "Notice_Bonus_Shorthanded_Eligibility",
            "Notice_Bonus_Shorthanded_Eligibility_Single",
            "Team_Cash_Award_Loser_Bonus_Neg",
            "Team_Cash_Award_Loser_Zero",
            "Team_Cash_Award_Rescued_Hostage",
            "Team_Cash_Award_Hostage_Interaction",
            "Team_Cash_Award_Hostage_Alive",
            "Team_Cash_Award_Planted_Bomb_But_Defused",
            "Team_Cash_Award_Survive_GuardianMode_Wave",
            "Team_Cash_Award_CT_VIP_Escaped",
            "Team_Cash_Award_T_VIP_Killed",
            "Team_Cash_Award_no_income",
            "Team_Cash_Award_no_income_suicide",
            "Team_Cash_Award_Generic",
            "Team_Cash_Award_Custom"
        ];
        private static readonly string[] SavedbyArray = [
            "Chat_SavePlayer_Savior",
            "Chat_SavePlayer_Spectator",
            "Chat_SavePlayer_Saved"
        ];
        private static readonly string[] TeamWarningArray = [
            "Cstrike_TitlesTXT_Game_teammate_attack",
            "Cstrike_TitlesTXT_Game_teammate_kills",
            "Cstrike_TitlesTXT_Hint_careful_around_teammates",
            "Cstrike_TitlesTXT_Hint_try_not_to_injure_teammates",
            "Cstrike_TitlesTXT_Killed_Teammate",
            "SFUI_Notice_Game_teammate_kills",
            "SFUI_Notice_Hint_careful_around_teammates",
            "SFUI_Notice_Killed_Teammate"
        ];
        int IClientListener.ListenerVersion => IClientListener.ApiVersion;
        int IClientListener.ListenerPriority => 0;
    }
}
