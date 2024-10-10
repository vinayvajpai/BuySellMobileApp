﻿using System;
using System.Text.RegularExpressions;

namespace BuySell.Services
{
	public class CreditCardService
	{
		public CreditCardService()
		{
		}
        public static CreditCardType GetCreditCardType(string CreditCardNumber)
        {
            Regex regAmex = new Regex("^3[47][0-9]{13}$");
            Regex regBCGlobal = new Regex("^(6541|6556)[0-9]{12}$");
            Regex regCarteBlanche = new Regex("^389[0-9]{11}$");
            Regex regDinersClub = new Regex("^3(?:0[0-5]|[68][0-9])[0-9]{11}$");
            Regex regDiscover = new Regex("^65[4-9][0-9]{13}|64[4-9][0-9]{13}|6011[0-9]{12}|(622(?:12[6-9]|1[3-9][0-9]|[2-8][0-9][0-9]|9[01][0-9]|92[0-5])[0-9]{10})$");
            Regex regInstaPayment = new Regex("^63[7-9][0-9]{13}$");
            Regex regJCB = new Regex(@"^(?:2131|1800|35\d{3})\d{11}$");
            Regex regKoreanLocal = new Regex("^9[0-9]{15}$");
            Regex regLaser = new Regex("^(6304|6706|6709|6771)[0-9]{12,15}$");
            Regex regMaestro = new Regex("^(5018|5020|5038|6304|6759|6761|6763)[0-9]{8,15}$");
            Regex regMastercard = new Regex("^(5[1-5][0-9]{14}|2(22[1-9][0-9]{12}|2[3-9][0-9]{13}|[3-6][0-9]{14}|7[0-1][0-9]{13}|720[0-9]{12}))$");
            Regex regSolo = new Regex("^(6334|6767)[0-9]{12}|(6334|6767)[0-9]{14}|(6334|6767)[0-9]{15}$");
            Regex regSwitch = new Regex("^(4903|4905|4911|4936|6333|6759)[0-9]{12}|(4903|4905|4911|4936|6333|6759)[0-9]{14}|(4903|4905|4911|4936|6333|6759)[0-9]{15}|564182[0-9]{10}|564182[0-9]{12}|564182[0-9]{13}|633110[0-9]{10}|633110[0-9]{12}|633110[0-9]{13}$");
            Regex regUnionPay = new Regex("^(62[0-9]{14,17})$");
            Regex regVisa = new Regex("^4[0-9]{12}(?:[0-9]{3})?$");
            Regex regVisaMasterCard = new Regex("^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14})$");

            if (regAmex.IsMatch(CreditCardNumber))
                return CreditCardType.amex;
            else if (regBCGlobal.IsMatch(CreditCardNumber))
                return CreditCardType.BCGlobal;
            else if (regCarteBlanche.IsMatch(CreditCardNumber))
                return CreditCardType.CarteBlanche;
            else if (regDinersClub.IsMatch(CreditCardNumber))
                return CreditCardType.diners;
            else if (regDiscover.IsMatch(CreditCardNumber))
                return CreditCardType.discover;
            else if (regInstaPayment.IsMatch(CreditCardNumber))
                return CreditCardType.InstaPayment;
            else if (regJCB.IsMatch(CreditCardNumber))
                return CreditCardType.JCB;
            else if (regKoreanLocal.IsMatch(CreditCardNumber))
                return CreditCardType.KoreanLocal;
            else if (regLaser.IsMatch(CreditCardNumber))
                return CreditCardType.Laser;
            else if (regMaestro.IsMatch(CreditCardNumber))
                return CreditCardType.Maestro;
            else if (regMastercard.IsMatch(CreditCardNumber))
                return CreditCardType.Mastercard;
            else if (regSolo.IsMatch(CreditCardNumber))
                return CreditCardType.Solo;
            else if (regSwitch.IsMatch(CreditCardNumber))
                return CreditCardType.Switch;
            else if (regUnionPay.IsMatch(CreditCardNumber))
                return CreditCardType.unionpay;
            else if (regVisa.IsMatch(CreditCardNumber))
                return CreditCardType.Visa;
            else if (regVisaMasterCard.IsMatch(CreditCardNumber))
                return CreditCardType.Visa;
            else
                return CreditCardType.Invalid;
        }
    }
    public enum CreditCardType
    {
        amex,
        BCGlobal,
        CarteBlanche,
        diners,
        discover,
        InstaPayment,
        JCB,
        KoreanLocal,
        Laser,
        Maestro,
        Mastercard,
        Solo,
        Switch,
        unionpay,
        Visa,
        VisaMasterCard,
        Invalid
    }
}

