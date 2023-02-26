using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutomationUtil
{
    public class MessageEditor
    {
        private long _maxIndex;

        public MessageEditor(long maxIndex=99)
        {
            _maxIndex = maxIndex;
            
        }

        public IEnumerable<string> GetNewMessage(IEnumerable<string> messages, int[] indices, long delta)
        {
            foreach (var message in messages)
            {
                var tu = TextUtil.ExtractDigits(message);
                var formatString = tu.Item1;
                var values = tu.Item2;
                if (values.Length == 0)
                {
                    yield return message;
                }
                else
                {
                    var targetIndices = indices.Select(i => values.Length - 1 - i);

                    // indexに加算・減算をした後、string.Formatのparams object[]にそのまま渡せるようにobject[]にcast
                    var newValues = values.Select((str, i) => targetIndices.Contains(i) ? AddIndex(Convert.ToInt64(str), delta).ToString() : str).Cast<object>().ToArray();
                    var newMessage = String.Format(formatString,newValues);
                    yield return newMessage;
                }
            }
        }


        public long AddIndex(long value, long delta)
        {
            var rtn = Util.MMod(value + delta, _maxIndex + 1);
            return rtn;

        }
    }
}
