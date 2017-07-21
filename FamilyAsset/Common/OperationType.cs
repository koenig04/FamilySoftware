using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum FunctionType
    {
        None,
        ItemConfig
    }

    public enum OperationType
    {
        Add,
        Modify,
        Delete,
        Search
    }

    public enum ItemType
    {
        None,//在SelectItem时，这个状态代表需要Load所有的ItemOne
        ItemOne,
        ItemTwo,
        Phrase
    }

    public class CommonEnumChsConverter
    {
        public static CommonEnumChsConverter Instance = new CommonEnumChsConverter();

        Dictionary<OperationType, string> m_dicOpType;
        Dictionary<ItemType, string> m_dicItemType;

        public CommonEnumChsConverter()
        {
            m_dicOpType = new Dictionary<OperationType, string>();
            m_dicOpType.Add(OperationType.Add, "添加");
            m_dicOpType.Add(OperationType.Modify, "修改");
            m_dicOpType.Add(OperationType.Delete, "删除");
            m_dicOpType.Add(OperationType.Search, "查找");
            m_dicItemType = new Dictionary<ItemType, string>();
            m_dicItemType.Add(ItemType.ItemOne, "一级分类");
            m_dicItemType.Add(ItemType.ItemTwo, "二级分类");
            m_dicItemType.Add(ItemType.Phrase, "常用语");
        }

        public string OperationTypeConvert(OperationType type)
        {
            string res = string.Empty;
            if (m_dicOpType.ContainsKey(type))
            {
                res = m_dicOpType[type];
            }
            return res;
        }

        public string ItemTypeConvert(ItemType type)
        {
            string res = string.Empty;
            if (m_dicItemType.ContainsKey(type))
            {
                res = m_dicItemType[type];
            }
            return res;
        }
    }
}
