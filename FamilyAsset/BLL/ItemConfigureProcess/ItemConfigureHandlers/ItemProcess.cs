using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL.ItemConfigureProcess
{
    public class ItemProcess
    {
        private JZItemOne m_dalItemOne = new JZItemOne();
        private JZItemTwo m_dalItemTwo = new JZItemTwo();
        private Phrase m_dalPhrase = new Phrase();

        /// <summary>
        /// 打开ItemConfig界面时，需要把所有的一级条目Load出来
        /// </summary>
        /// <param name="lstItemOne"></param>
        public void LoadItemOne(bool inOrOut, out List<Model.JZItemOne> lstItemOne)
        {
            lstItemOne = null;
            DataTable dt = m_dalItemOne.GetList(inOrOut);
            if (dt != null)
            {
                lstItemOne = (from d in dt.AsEnumerable()
                              select new Model.JZItemOne()
                              {
                                  JZItemOneID = d.Field<string>("JZItemOneID"),
                                  JZItemOneName = d.Field<string>("JZItemOneName"),
                                  IconName = d.Field<string>("IconName"),
                                  IconNamePressed = d.Field<string>("IconNamePressed"),
                                  IncomeOrCost = d.Field<bool>("IncomeOrCost")
                              }).ToList<Model.JZItemOne>();
            }
        }

        /// <summary>
        /// 当选择任意一个一级条目时，需要显示该一级条目所对应的二级条目和该一级条目对应的常用语
        /// </summary>
        /// <param name="ItemOneID"></param>
        /// <param name="lstItemtwo"></param>
        /// <param name="lstPhrase"></param>
        public void LoadItemTwoAndPhrase(string ItemOneID, out List<Model.JZItemTwo> lstItemtwo, out List<Model.Phrase> lstPhrase)
        {
            lstItemtwo = null;
            lstPhrase = null;

            DataTable dtItemTwo = m_dalItemTwo.GetList(ItemOneID);
            DataTable dtPhrase = m_dalPhrase.GetPhrase(ItemOneID);

            if (dtItemTwo != null)
            {
                lstItemtwo = (from d in dtItemTwo.AsEnumerable()
                              select new Model.JZItemTwo()
                              {
                                  JZItemOneID = d.Field<string>("JZItemOneID"),
                                  JZItemTwoID = d.Field<string>("JZItemTwoID"),
                                  JZItemTwoName = d.Field<string>("JZItemTwoName"),
                                  IconName = d.Field<string>("IconName"),
                                  IconNamePressed = d.Field<string>("IconNamePressed")
                              }).ToList<Model.JZItemTwo>();
            }
            if (dtPhrase != null)
            {
                lstPhrase = (from d in dtPhrase.AsEnumerable()
                             select new Model.Phrase()
                             {
                                 PhraseID = d.Field<string>("PhraseID"),
                                 PhraseContent = d.Field<string>("PhraseContent")
                             }).ToList<Model.Phrase>();
            }
        }

        /// <summary>
        /// 当点击二级条目时，需要显示该二级条目所对应的常用语
        /// </summary>
        /// <param name="ItemTwoID"></param>
        /// <param name="lstPhrase"></param>
        public void LoadPhrase(string ItemTwoID, out List<Model.Phrase> lstPhrase)
        {
            lstPhrase = null;

            DataTable dt = m_dalPhrase.GetPhrase(ItemTwoID);

            if (dt != null)
            {
                lstPhrase = (from d in dt.AsEnumerable()
                             select new Model.Phrase()
                             {
                                 PhraseID = d.Field<string>("PhraseID"),
                                 PhraseContent = d.Field<string>("PhraseContent")
                             }).ToList<Model.Phrase>();
            }
        }

        /// <summary>
        /// 增加一级条目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddItemOne(Model.JZItemOne model, out string itemOneID)
        {
            return m_dalItemOne.Add(model, out itemOneID);
        }

        /// <summary>
        /// 增加二级条目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddItemTwo(Model.JZItemTwo model, out string itemTwoID)
        {
            return m_dalItemTwo.Add(model, out itemTwoID);
        }

        /// <summary>
        /// 增加常用语
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddPhrase(Model.Phrase model, out string phraseID)
        {
            return m_dalPhrase.Add(model, out phraseID);
        }

        /// <summary>
        /// 修改一级条目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateItemOne(Model.JZItemOne model)
        {
            return m_dalItemOne.Update(model);
        }

        /// <summary>
        /// 修改二级条目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateItemTwo(Model.JZItemTwo model)
        {
            return m_dalItemTwo.Update(model);
        }

        /// <summary>
        /// 修改常用语
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdatePhrase(Model.Phrase model)
        {
            return m_dalPhrase.Update(model);
        }

        /// <summary>
        /// 删除一级条目
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DelItemOne(string Id)
        {
            return m_dalItemOne.Del(Id);
        }

        /// <summary>
        /// 删除二级条目
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DelItemTwo(string Id)
        {
            return m_dalItemTwo.Del(Id);
        }

        /// <summary>
        /// 删除常用语
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DelPhrase(string Id)
        {
            return m_dalPhrase.Del(Id);
        }
    }
}
