using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BreakInfinity;

public class Data
{
    public BigDouble clicks;

    public List<int> clickUpgradeLevel;
    public List<int> productionUpgradeLevel;

    public Data()
    {
        clicks = 0;

        clickUpgradeLevel = new int[4].ToList();
        productionUpgradeLevel = new int[4].ToList();
    }
}
