using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShapeTemplateLib.Templates.User0
{

    public partial class HolePlacementTemplate
    {
        private partial class ParseOffsetAndSize
        {        
            /// <summary>
            /// 
            /// </summary>
            /// <param name="sValue"></param>
            /// <returns></returns>
            public bool DecodeString(string sValue)
            {
                //  Decompose formatting string
                string[] components = sValue.Split(";".ToCharArray());

                foreach (string s in components)
                {
                    if (s.Trim() == "") continue;

                    string[] parts = s.Split("=".ToCharArray());
                    if (parts.Length != 2)
                    {
                        StatusMessageList.Add("Invalid tag, must have key=value");
                        return false; 
                    }

                    if (!DecodeKey(parts[0], parts[1]))
                    {
                        StatusMessageList.Add($"Error decoding tag {parts[0]}");
                        return false;
                    }
                }

                return true;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="key"></param>
            /// <param name="value"></param>
            /// <returns></returns>
            protected bool DecodeKey(string key, string value)
            {
                eTagWithValue ekey = GetTagType(key);

                // if this key is already present error
                if (TagValueList.Where(m => m.Item1 == ekey).FirstOrDefault() != null)
                {
                    StatusMessageList.Add($"duplicate parameter {key}");
                    return false;
                }

                double dValue = 0;

                switch (ekey)
                {
                    case eTagWithValue.Width:
                    case eTagWithValue.Left:
                    case eTagWithValue.Right:
                    case eTagWithValue.Center:
                        if (!DecodeValue(value, GridCellWidth, out dValue))
                        {
                            StatusMessageList.Add($"Error parsing {key}");
                            return false;
                        }
                        TagValueList.Add(new Tuple<eTagWithValue, double>(ekey, dValue));
                        return true;

                    case eTagWithValue.Height:
                    case eTagWithValue.Top:
                    case eTagWithValue.Middle:
                    case eTagWithValue.Bottom:
                        if (!DecodeValue(value,GridCellHeight ,out dValue))
                        {
                            StatusMessageList.Add($"Error parsing {key}");
                            return false;
                        }
                        TagValueList.Add(new Tuple<eTagWithValue, double>(ekey, dValue));

                        return true;
                    case eTagWithValue.Unrecognized:
                        StatusMessageList.Add($"Unrecognized tag {key}");
                        return false;
                }

                return false;
            }

   
            /// <summary>
            /// 
            /// </summary>
            /// <param name="value"></param>
            /// <param name="Max"></param>
            /// <param name="OutValue"></param>
            /// <returns></returns>
            private bool DecodeValue(string value, double Max, out double OutValue)
            {
                OutValue = 0;

                bool IsPercent = false;

                if (value.Contains("%"))
                {
                    value = value.Replace("%", "");
                    IsPercent = true;
                }

                if (!double.TryParse(value, out OutValue))
                {
                    StatusMessageList.Add($"Invalid numeric value '{value}'");
                    return false;
                }

                if (OutValue < 0)
                {
                    StatusMessageList.Add($"Positive value required '{value}'");
                    return false;
                }
                if (IsPercent)
                {
                    if (OutValue > 100)
                    {
                        StatusMessageList.Add($"Percent must be <= 100 '{value}'");
                        return false;
                    }
                    OutValue = OutValue / 100 * Max;
                }

                return true;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            protected eTagWithValue GetTagType(string value)
            {
                switch (value.Trim())
                {
                    case "width":
                        return eTagWithValue.Width;
                    case "height":
                        return eTagWithValue.Height;
                    case "left":
                        return eTagWithValue.Left;
                    case "right":
                        return eTagWithValue.Right;
                    case "center":
                        return eTagWithValue.Center;
                    case "top":
                        return eTagWithValue.Top;
                    case "middle":
                        return eTagWithValue.Middle;
                    case "bottom":
                        return eTagWithValue.Bottom;
                    default:
                        return eTagWithValue.Unrecognized;
                }
            }

        }

    }
}
