using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Stats.Model;

public class StatUI : MonoBehaviour
{
    [SerializeField]
    private Text _healthText,_agilityText,_attackText,_defenseText;

    [SerializeField]
    private StatsSO _statData;

    public void UpdateStat(StatType statType, int newVal)
    {
        switch(statType)
            {
                case StatType.HEALTH:
                    int oldValHealth = int.Parse(_healthText.text);
                    _healthText.text = (oldValHealth+newVal).ToString();
                    break;
                case StatType.AGILITY:
                     int oldValAgility = int.Parse(_agilityText.text);
                    _agilityText.text = (oldValAgility+newVal).ToString();
                    break;
                case StatType.ATTACK:
                     int oldValAttack = int.Parse(_attackText.text);
                    _attackText.text = (oldValAttack+newVal).ToString();
                    break;
                case StatType.DEFENSE:
                     int oldValDefense = int.Parse(_defenseText.text);
                    _defenseText.text = (oldValDefense+newVal).ToString();
                    break;
            }
    }

    public void Update()
    {
        _healthText.text = (_statData.GetStatFor(StatType.HEALTH)).ToString();
        _agilityText.text = (_statData.GetStatFor(StatType.AGILITY)).ToString();
        _attackText.text = (_statData.GetStatFor(StatType.ATTACK)).ToString();
        _defenseText.text = (_statData.GetStatFor(StatType.DEFENSE)).ToString();

    }
}
