﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Writer
    {
        [Key]
        public int WriterID { get; set; }
        [StringLength(50)]
        public string WriterName { get; set; }
        [StringLength(50)]
        public string WriterSurname { get; set; }
        [StringLength(250)]
        public string WriterImage { get; set; }
        [StringLength(100)]
        public string WriterAbout { get; set; }
        [StringLength(50)]
        public string WriterMail { get; set; }
        public byte[] WriterPasswordHash { get; set; }
        public byte[] WriterPasswordSalt { get; set; }
        [StringLength(50)]
        public string WriterTitle { get; set; }

        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }

        public bool WriterStatus { get; set; }

        //Yazar -Başlık İlişkisi
        public ICollection<Heading> Headings { get; set; }
        //Yazar -Content İlişkisi
        public ICollection<Content> Contents { get; set; }

    }
}
