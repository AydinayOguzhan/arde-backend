﻿using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CurrencyManager : ICurrencyService
    {
        private ICurrencyDal _currencyDal;

        public CurrencyManager(ICurrencyDal currencyDal)
        {
            _currencyDal = currencyDal;
        }

        public IDataResult<IList<Currency>> GetAll()
        {
            return new SuccessDataResult<IList<Currency>>(_currencyDal.GetList());
        }

        public IDataResult<Currency> GetByName(string name)
        {
            return new SuccessDataResult<Currency>(_currencyDal.Get(c => c.Name == name));
        }
    }
}
