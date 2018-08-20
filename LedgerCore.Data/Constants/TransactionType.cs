using System;
using System.Collections.Generic;
using System.Text;

namespace LedgerCore.Data.Constants
{
    public static class TransactionType
    {
        public enum Type
        {
            Expense = 0,
            Income,
            Transfer
        }
    }
}
