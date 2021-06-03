using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BreakInfinity;

[Serializable]
public class Data
{
    public BigDouble clicks;

    public List<int> clickUpgradeLevel;
    public List<int> productionUpgradeLevel;
    public List<int> monstersUnlocked;
    //public List<int> monstersUnlocked;

    public Data()
    {
        clicks = 0;

        clickUpgradeLevel = new int[4].ToList();
        productionUpgradeLevel = new int[4].ToList();
        monstersUnlocked = new int[4].ToList();
        //monstersUnlocked = Enumerable.Repeat(false, 4).ToList();
    }
}
