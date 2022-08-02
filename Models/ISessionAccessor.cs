using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using AutoMapper;

namespace CBTour.Models
{
   public interface ISessionAccessor
    {
        void Clear();

        void SetSessionAccessor(UserVM user);
    }
}
