using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtilityClasses
{
    /// <summary>
    /// Class to generate a delimited list, with configurable delimeter, optional prefix and postfix for the entire list as well as each item in the list.
    /// </summary>
    public class DelimitedList
    {
        private readonly StringBuilder _list = new StringBuilder();
        private readonly string _delimeter = ", ";
        private readonly string _prefix = "";
        private readonly string _postfix = "";
        private readonly string _itemPrefix = "";
        private readonly string _itemPostfix = "";
        private readonly List<DelimitedList> _insertedDelimitedLists = new List<DelimitedList>();
        private readonly int _insertPosition = 0;
        public int CreationOrder = 0;
        private readonly bool _generateIfEmpty = false;

        public DelimitedList()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelimitedList" /> class.
        /// </summary>
        /// <param name="pInsertPosition">The p insert position.</param>
        /// <param name="pPrefix">The symbol or string to prefix on this list.</param>
        /// <param name="pPostfix">The symbol or string to append to the end of this list.</param>
        /// <param name="pGenerateIfEmpty">Should we still generate an empty list if no items present?</param>
        /// <param name="pItemPrefix">The item prefix.</param>
        /// <param name="pItemPostfix">The item suffix.</param>
        public DelimitedList(int pInsertPosition = 0, string pPrefix = "", string pPostfix = "",
            bool pGenerateIfEmpty = false, string pItemPrefix = "", string pItemPostfix = "", string pDelimeter = ", ")
        {
            _insertPosition = pInsertPosition;
            _prefix = pPrefix;
            _postfix = pPostfix;
            _itemPrefix = pItemPrefix;
            _itemPostfix = pItemPostfix;
            _generateIfEmpty = pGenerateIfEmpty;
            _delimeter = pDelimeter;
        }

        public DelimitedList(string pDelimeter = ", ")
        {
            _delimeter = pDelimeter;
        }

        public int InsertPosition
        {
            get
            {
                return this._insertPosition;
            }
        }

        // Append text to the end of the delimited list
        public void AppendString(string pStringToAppend)
        {
            if (pStringToAppend != "")
            {
                //if ((_list.ToString() != "") || (_insertedDelimitedLists.Count > 0))
                if ((_list.ToString() != ""))
                {
                    _list.Append(_delimeter);
                }
                _list.Append(_itemPrefix);
                _list.Append(pStringToAppend);
                _list.Append(_itemPostfix);
            }
        }

        // Append a delimited list
        public DelimitedList AppendDelimitedList(string pPrefix = "", string pPostfix = "",
            bool pGenerateIfEmpty = false, string pItemPrefix = "", string pItemPostfix = "", string pDelimeter = ", ")
        {
            int InsertPosition = _list.Length;
            if (InsertPosition < 0)
                InsertPosition = 0;

            DelimitedList Result = new DelimitedList(InsertPosition, pPrefix, pPostfix, pGenerateIfEmpty, pItemPrefix, pItemPostfix, pDelimeter);
            Result.CreationOrder = _insertedDelimitedLists.Count + 1;
            _insertedDelimitedLists.Add(Result);
            return Result;
        }

        public string AsString()
        {
            string Result = "";
            string CurrentListOutput;

            if (_insertedDelimitedLists.Count > 0)
            {
                foreach (DelimitedList ThisDelimitedList in
                    _insertedDelimitedLists.OrderByDescending(DL => DL.InsertPosition).ThenByDescending(DL => DL.CreationOrder))
                {
                    CurrentListOutput = ThisDelimitedList.AsString();

                    if (CurrentListOutput != "")
                    {
                        if (ThisDelimitedList.InsertPosition >= _list.ToString().Length)
                        {
                            if (_list.ToString() != "")
                                _list.Append(_delimeter);
                            _list.Append(_itemPrefix);
                            _list.Append(CurrentListOutput);
                            _list.Append(_itemPostfix);
                        }
                        else
                        {
                            if ((_list.ToString().Substring(ThisDelimitedList.InsertPosition) != "") && (_list.ToString().Substring(ThisDelimitedList.InsertPosition, _delimeter.Length) != _delimeter))
                            {
                                _list.Insert(ThisDelimitedList.InsertPosition, _delimeter);
                            }
                            _list.Append(_itemPostfix);
                            _list.Insert(ThisDelimitedList.InsertPosition, CurrentListOutput);
                            _list.Append(_itemPrefix);
                            if (_list.ToString().Substring(0, ThisDelimitedList.InsertPosition) != "")
                            {
                                _list.Insert(ThisDelimitedList.InsertPosition, _delimeter);
                            }
                        }
                    }

                }
            }
            Result = _list.ToString();

            if ((Result != "") || (_generateIfEmpty))
            {
                Result = _prefix + Result + _postfix;
            }

            return Result;
        }
    }
}