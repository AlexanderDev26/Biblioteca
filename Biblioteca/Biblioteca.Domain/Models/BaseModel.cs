﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Models
{
    public abstract class BaseModel
    {
        public int Id { get; set; }

        protected BaseModel(int id) => Id = id;
    }
}
