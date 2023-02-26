using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modding;
using UnityEngine;

namespace AutomationUtil
{
    public class CommonController : MonoBehaviour
    {
        private ModKey _incrKey;
        private ModKey _decrKey;

        public AudioSource incrSound;
        public AudioSource decrSound;

        public void Start()
        {
            _incrKey = ModKeys.GetKey("message-increment");
            _decrKey = ModKeys.GetKey("message-decrement");

            incrSound = gameObject.AddComponent<AudioSource>();
            decrSound = gameObject.AddComponent<AudioSource>();
            incrSound.loop = false;
            incrSound.volume = 1.0F;
            decrSound.loop = false;
            decrSound.volume = 1.0F;
            decrSound.pitch = 0.5F;

            incrSound.clip = ModResource.GetAudioClip("incr").AudioClip;
            decrSound.clip = ModResource.GetAudioClip("incr").AudioClip;

        }

        public static string getMessageStr(MKey mKey)
        {
            var messageStr = "(None)";
            if (mKey.useMessage)
            {
                var mess = MKey.CombineVariables(mKey.message);
                messageStr = $"{{{mess}}}";
            }

            return messageStr;
        }

        public void Update()
        {
            bool unavailable = !Mod.IsAvailableScene() || ReferenceMaster.activeMachineSimulating || StatMaster.Mode.selectedTool == StatMaster.Tool.Modify;
            if (!unavailable)
            {
                if (_incrKey.IsPressed)
                {
                    UpdateSelectedBlockMessage(new[] { 0 }, 1);
                }
                else if (_decrKey.IsPressed)
                {
                    UpdateSelectedBlockMessage(new[] { 0 }, -1);
                }
            }

        }

        public void UpdateSelectedBlockMessage(int[] indices, long delta)
        {
            var editor = new MessageEditor();

            bool isApplied = false;

            var selectedBlockCount = AdvancedBlockEditor.Instance.selectionController.Selection.Count;
            foreach (var bh in AdvancedBlockEditor.Instance.selectionController.MachineSelection)
            {
                //Mod.Log($"Block-{bh.name}-{bh.BlockID}");

                foreach (var mt in bh.MapperTypes)
                {
                    Mod.Log($"\tmapper-{nameof(mt)}-{mt}-{mt.Key}-{mt.DisplayName}");
                    if (mt is MKey kmt)
                    {
                        //var kmt = mt as MKey;

                        Mod.Log($"\tkeyinfo:{CommonController.getMessageStr(kmt)} {kmt.useMessage}");

                        if (kmt.useMessage)
                        {
                            var newMessages = editor.GetNewMessage(kmt.message, indices, delta).ToArray();
                            kmt.message = newMessages;
                            bh.Load(kmt.Serialize());
                            var xDataHolder = new XDataHolder();
                            bh.OnSave(xDataHolder);

                            isApplied = true;
                        }
                        Mod.Log("Set new messages.");
                    }
                }
            }
            if (isApplied)
            {
                incrSound.Play();
            }

        }


    }
}
