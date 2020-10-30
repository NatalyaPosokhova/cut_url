using Cut_URL.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cut_URL.Business_Logic
{
    public class CutUrlLogic
    {
        private IRepository _repository;
        public CutUrlLogic(IRepository repository)
        {
            _repository = repository;
        }
    }
}
