using UnityEngine;
using UnityEngine.Events;
public class Health : MonoBehaviour
{
    #region Serialized
    [Tooltip("The max health that the object will have.")]
    [field: SerializeField] public int MaxHealth { get; private set; } = 100;
    [Tooltip("The Unity event to be invoked when the health reaches or is below 0 after the OnDamage event is invoked.")]
    [SerializeField] private UnityEvent _onDeath = new();
    [Tooltip("The Unity event to be invoked when the object is damaged.")]
    [SerializeField] private UnityEvent _onDamage = new();
    [Tooltip("The Unity event to be invoked when the object is healed.")]
    [SerializeField] private UnityEvent _onHeal = new();
    [Tooltip("While true, prevents the loss of health.")]
    [SerializeField] private bool _invulnerable;
    #endregion Serialized
    public int CurrentHealth { get; private set; } = 0;

    #region Methods
    private void OnValidate()
    {
        if (MaxHealth < 0)
        {
            MaxHealth = 1;
            Debug.LogWarning("Max health cannot be 0 or less. Setting max health to 1.");
        }
    }

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    /// <summary>
    /// Subtracts the current health of the object by the provide amount.
    /// </summary>
    /// <param name="amount">The int value that is used to subtract from the current health of the object</param>
    /// <returns>true if damaged successfully, false otherwise.</returns>
    public bool Damage(int amount)
    {
        if(amount <= 0)
        {
            Debug.LogWarning("Damage amount must be greater than 0!");
            return false;
        }
        if(_invulnerable)
        {
            return false;
        }
        if (CurrentHealth - amount < 0)
        {
            CurrentHealth = 0;
        }
        else
        {
            CurrentHealth -= amount;
        }
        _onDamage?.Invoke();
        return true;
    }
    /// <summary>
    /// Does addition to the current health by the given value
    /// </summary>
    /// <param name="amount">The int value that is used to added to the current health of the object</param>
    /// <returns>true if healed successfully, false otherwise.</returns>
    public bool Heal(int amount)
    {
        if (amount <= 0)
        {
            Debug.LogWarning("Heal amount must be greater than 0!");
            return false;
        }
        if(CurrentHealth == MaxHealth)
        {
            return false;
        }
        if (CurrentHealth + amount > MaxHealth)
        {
            CurrentHealth = MaxHealth;
            return true;
        }
        CurrentHealth += amount;
        _onHeal?.Invoke();
        return true;
    }

    public void SetHealth(int amount, bool increaseMaxHealth)
    {
        if (amount < 0)
        {
            Debug.LogWarning("Health amount cannot be less than 0!");
            return;
        }
        CurrentHealth = amount;
        if (increaseMaxHealth && amount > MaxHealth)
        {
            MaxHealth = amount;
        }
    }

    public void BeInvulnerable(bool value)
    {
        _invulnerable = value;
    }

    public void SubscribeToOnDeath(UnityAction action)
    {
        _onDeath.AddListener(action);
    }

    public void SubscribeToOnDamage(UnityAction action)
    {
        _onDamage.AddListener(action);
    }

    public void SubscribeToOnHeal(UnityAction action)
    {
        _onHeal.AddListener(action);
    }

    public void UnsubscribeFromOnDeath(UnityAction action)
    {
        _onDeath.RemoveListener(action);
    }

    public void UnsubscribeFromOnDamage(UnityAction action)
    {
        _onDamage.RemoveListener(action);
    }

    public void UnsubscribeFromOnHeal(UnityAction action)
    {
        _onHeal.RemoveListener(action);
    }
    #endregion Methods

}


