using System;
using System.Collections.Generic;
using System.Text;

namespace StockMarket
{
    public class CompanyNameGenerator
    {
        private List<string> _firstWord = new List<string>()
        {
            "general", "creative", "intuitous", "original", "classic", "old", "common", "esoteric", "technical", "open", "dominant", "distinct", "accurate", "interesting",
            "reusable", "accessible", "interoperable" ,"ripe", "adaptable", "judicious", "robust", "advisable", "knowledgeable", "safe", "aesthetically Pleasing", "known",  "satisfying",
            "agreeable", "large", "scalable", "available",   "lean"  ,  "scarce" ,"balanced"  ,  "learnable" ,  "secure", "bright",  "light",   "selective" ,"calm" ,  "likable" ,
            "serious", "candid" , "literate" ,   "sharp", "capable" ,"logical", "silent" ,"certified" ,  "long", "lasting" ,"simple" ,"clear" ,  "longterm"  , "sincere" ,"compliant"  , "lyrical" ,"skillful",
            "cooperative", "magical", "small", "coordinated", "maintainable" ,   "smart" ,"courageous" , "makeshift" ,  "smooth", "credible",    "material"   , "solid", "cultured" ,"mature",
            "sophisticated" ,"curious" ,"mixed",   "special"  ,"decisive" ,   "momentous",   "spectacular" , "deep" ,   "mysterious" , "spotless" ,"delightful" , "natural","stable" ,"deployable" ,
            "necessary" ,  "standard" ,"descriptive" ,"new", "steadfast", "detailed"    ,"next"    ,"steady", "different" ,  "nimble" , "strategic" ,"diligent" ,  "obtainable" , "strong",
            "distinct"   , "odd", "sturdy" ,"dominant"  , "offbeat", "stylish", "dramatic"  ,  "open"  ,  "substantial", "dry", "operable"  ,  "subtle", "durable", "optimal","successful",
            "dynamic", "organic" ,"succinct", "economical",  "outstanding", "sudden", "educated"  ,  "overt" ,  "suitable", "efficient"  , "painstaking", "superb", "elastic", "panoramic"  , "supreme",
            "eloquent"  ,  "parallel"  ,  "sustainable", "energetic"  , "peaceful"  ,  "swanky", "entertaining"   , "perfect" ,"talented", "enthusiastic"  ,  "periodic" ,   "tame",
            "familiar"   , "perpetual" ,  "tangible", "famous",  "physical"  ,  "tasteful", "fast"   , "plausible" ,  "tasty", "fearless"  ,  "popular", "tested",
            "festive", "possible"   , "thankful", "fierce" , "powerful"   , "thin", "fine"   , "precious"  ,  "thinkable", "flawless"  ,  "premium", "thoughtful",
            "flowing", "present", "threatening", "focused", "private", "timely", "frequent"  ,  "productive", "traceable", "fresh" ,"protective",  "truthful",
            "friendly" ,   "proud" ,"typical", "functional" ,"public", "ubiquitous", "funny" ,  "quick" ,"unbiased", "futuristic" ,"quiet" ,  "uncovered", "gainful", "rare", "unique",
            "good", "ready"  , "unknown", "grounded"   , "real" ,"upbeat", "real", "time" ,"upscale", "harmonious", "rebel"  , "usable", "helpful" ,"receptive", "useful",
            "holistic", "redundant"  , "valuable", "hybrid" , "regular", "vast", "important" ,"relaxed", "well-made", "inexpensive", "relevant" ,"wide", "inquisitive","reliable" , "wise",
            "instinctive" ,"remarkable" ,"workable", "intelligent" ,"resilient"   ,"youthful"
        };
        private List<string> _secondWord = new List<string>()
        {
            "industries", "computers", "art", "world", "information", "map", "two", "family", "government", "health", "system", "meat", "year", "music", "reading", "data", "food", "understanding",
            "theory", "law", "bird", "literature", "problem", "software", "control", "knowledge", "power", "ability", "economics", "internet", "television", "science", "library", "nature", "fact",
            "idea", "product", "idea", "investment", "activity", "media", "community", "safety", "quility", "development", "language", "management", "player", "variety", "video", "security",
            "organization", "physics", "policy", "series", "thought", "basis", "direction", "strategy", "army", "camera", "freedom", "paper", "environment", "child", "instance", "truth",
            "marketing", "writing", "difference", "goal", "news", "audience", "fishing", "growth", "income", "marriage", "user", "combination", "failure", "meaning", "medicine", "philosophy",
            "communication", "chemistry", "disk", "energy", "advertising", "addition", "math", "moment", "politics", "attention", "decision", "event", "property", "shopping", "wood", "competition"
        };
        private List<string> _thirdWord = new List<string>()
        {
            "Inc.", "LLC", "RCC", "PLC", "PC", "UC", "LLP", "Corp.", "Ltd", "Bank", ""
        };

        public List<Company> GetGeneratedCompanies()
        {
            Random rnd = new Random();
            List<Company> companies = new List<Company>();
            int count = 0;
            string firstWord;
            string secondWord;
            string thirdWord;
            string company;
            while (count < 15)
            {
                firstWord = UppercaseFirst(_firstWord[rnd.Next(0, _firstWord.Count - 1)]);
                secondWord = UppercaseFirst(_secondWord[rnd.Next(0, _secondWord.Count - 1)]);
                thirdWord = "";
                int chance = rnd.Next(0, 100);
                if (chance <= 70)
                {
                    thirdWord = _thirdWord[rnd.Next(0, _thirdWord.Count - 1)];
                    company = firstWord + " " + secondWord + " " + thirdWord;
                }
                else
                {
                    company = firstWord + " " + secondWord;
                }
                double value = setInitialValue(company);
                int shares = setInitialShares(value, company);
                companies.Add(new Company(company, value, shares));
                count++;
            }
            return companies;
        }

        private static string UppercaseFirst(string s)
        {
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        private double setInitialValue(string comName)
        {
            Random rnd = new Random();
            int ranNum = rnd.Next(0, 100);
            if (ranNum <= 33)
            {
                return Math.Round(comName.Length * 11.3, 2, MidpointRounding.AwayFromZero);
            }
            else if (ranNum >= 34 && ranNum <= 66)
            {
                return Math.Round(comName.Length * 6.6, 2, MidpointRounding.AwayFromZero);
            }
            else if (ranNum >= 67)
            {
                return Math.Round(comName.Length * 15.4, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                return Math.Round(comName.Length * 3.3, 2, MidpointRounding.AwayFromZero);
            }

        }

        private int setInitialShares(double comValue, string comName)
        {
            return Convert.ToInt32((comValue * 3) * ((comName.Length * 3) / 12));
        }

        public List<Company> UpdateSharePrice(List<Company> generated)
        {
            Random rnd = new Random();
            int upOrDown;
            int jumpOrFall;

            foreach (Company comp in generated)
            {
                upOrDown = rnd.Next(0, 100);
                jumpOrFall = rnd.Next(0, 100);
                if (comp.Value < 0.5)
                {
                    comp.Value += 2.5;
                }
                if (upOrDown <= 50)
                {
                    if (jumpOrFall <= 10)
                    {
                        comp.Value = Math.Round(comp.Value / (rnd.NextDouble() * rnd.Next(50, 125)), 2, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        comp.Value = Math.Round(comp.Value / (rnd.NextDouble() * rnd.Next(5, 15)), 2, MidpointRounding.AwayFromZero);
                    }
                }
                else if (upOrDown >= 51)
                {
                    if (jumpOrFall <= 10)
                    {
                        comp.Value = Math.Round(comp.Value * (rnd.NextDouble() * rnd.Next(75, 125)), 2, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        comp.Value = Math.Round(comp.Value * (rnd.NextDouble() * rnd.Next(5, 15)), 2, MidpointRounding.AwayFromZero);
                    }
                }
            }
            return generated;
        }
    }
}
