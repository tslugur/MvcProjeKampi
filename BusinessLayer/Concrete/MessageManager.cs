﻿using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class MessageManager : IMessageService
    {
        IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public Message GetByID(int id)
        {
            return _messageDal.Get(x => x.MessageID == id);
        }

      

        public List<Message> GetListDraft(string p)
        {
            return _messageDal.List(x => x.SenderMail == p).Where(y=>y.isDraft==true).ToList();
        }

        public List<Message> GetListInbox(string p)
        {
            return _messageDal.List(x => x.ReceiverMail == p);
        }

     

        public List<Message> GetReadList(string p)
        {
            return _messageDal.List(x => x.ReceiverMail == p).Where(y => y.MessageRead == true).ToList();
        }

        public List<Message> GetSendbox(string p)
        {
            return _messageDal.List(x => x.SenderMail == p);
        }

     

        public List<Message> GetUnReadList(string p)
        {
            return _messageDal.List(x => x.ReceiverMail == p).Where(y=>y.MessageRead==false).ToList();
        }

        public void MessageAddBL(Message message)
        {
            _messageDal.Insert(message);
        }

        public void MessageDelete(Message message)
        {
            throw new NotImplementedException();
        }

        public void MessageUpdate(Message message)
        {
            _messageDal.Update(message);
        }
    }
}
