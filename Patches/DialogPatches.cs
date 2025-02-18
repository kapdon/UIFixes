﻿using Aki.Reflection.Patching;
using EFT.UI;
using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

namespace UIFixes
{
    public class DialogPatch : ModulePatch
    {
        private static MethodInfo AcceptMethod;

        protected override MethodBase GetTargetMethod()
        {
            Type dialogWindowType = typeof(MessageWindow).BaseType;
            AcceptMethod = AccessTools.Method(dialogWindowType, "Accept");

            return AccessTools.Method(dialogWindowType, "Update");
        }

        [PatchPostfix]
        private static void Postfix(object __instance, bool ___bool_0)
        {
            if (!___bool_0)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                AcceptMethod.Invoke(__instance, []);
                return;
            }
        }
    }
}
