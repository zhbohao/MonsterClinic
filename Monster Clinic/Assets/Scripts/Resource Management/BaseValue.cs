/// <summary>
/// Base value class of game
/// Base class , parent class of Glitter.cs, Power.cs, TechnologyPoint.cs
/// Sept.26 2013
/// By Chanberlen
/// 
/// </summary> 
public class BaseValue {
	private int _totalValue; // total value
	private int _currentValue; // current value
	private float _updateRatio;
	
	public int TotalValue { get; set; } // adjust _totalValue
	public int CurrentValue { get; set; } // adjust _currentValue
	public float UpateRatio { get; set; } // adjust _updateRatio
	
	public void setUpdateBaseValue () {
		_totalValue = (int)(_totalValue * _updateRatio);
		_currentValue = _totalValue;
	}
}
