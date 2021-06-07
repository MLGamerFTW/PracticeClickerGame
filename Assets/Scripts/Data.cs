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
    public List<bool> monstersUnlocked;

    public Data()
    {
        clicks = 0;

        clickUpgradeLevel = new int[4].ToList();
        productionUpgradeLevel = new int[4].ToList();
        monstersUnlocked = new bool[4].ToList();
    }
}
