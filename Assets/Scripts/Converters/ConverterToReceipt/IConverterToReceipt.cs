using System.Collections.Generic;
using Game.Production.Config;
using Game.Production.Model;

namespace Game.Production.Converters
{
    internal interface IConverterToReceipt
    {
        List<Receipt> ConvertToList(List<ReceiptContent> list);
        Receipt ConvertToReceipt(ReceiptContent content);
    }   
}

