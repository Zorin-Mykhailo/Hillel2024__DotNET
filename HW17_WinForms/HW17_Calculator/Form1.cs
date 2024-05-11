using System.Globalization;
using System.Text;

namespace HW17_Calculator;

public partial class Calc : Form
{
    enum ButtonType
    {
        Digits,
        Operation,
        Clear,
        Memory,
        UnknownType
    }

    Stack<CalcItem> _calcStack = new Stack<CalcItem>();

    private double? _memory = null;

    private static readonly Dictionary<string, string> _digitBtns;

    private static readonly Dictionary<string, ECalcOperation> _opButtons;

    private static readonly HashSet<string> _tags_clearButtons;

    private static readonly HashSet<string> _tags_memoryButtons;

    static Calc()
    {
        _digitBtns = new Dictionary<string, string>()
            {
                { "Btn_Dot", "." },
                { "Btn_0", "0" },
                { "Btn_1", "1" },
                { "Btn_2", "2" },
                { "Btn_3", "3" },
                { "Btn_4", "4" },
                { "Btn_5", "5" },
                { "Btn_6", "6" },
                { "Btn_7", "7" },
                { "Btn_8", "8" },
                { "Btn_9", "9" },
            };

        _opButtons = new Dictionary<string, ECalcOperation>()
            {
                { "Btn_Inverse", ECalcOperation.Inverse },
                { "Btn_OpDivide", ECalcOperation.Divide },
                { "Btn_OpMultiply", ECalcOperation.Multiply },
                { "Btn_OpMinus", ECalcOperation.Minus },
                { "Btn_OpPlus", ECalcOperation.Plus },
                { "Btn_Sqrt", ECalcOperation.Sqrt },
                { "Btn_Percent", ECalcOperation.Percent },
                { "Btn_Fraction", ECalcOperation.Fraction },
                { "Btn_Equals", ECalcOperation.Equals },
            };

        _tags_clearButtons = new HashSet<string>() { "Btn_CE", "Btn_Clear", "Btn_Delete" };

        _tags_memoryButtons = new HashSet<string>() { "Btn_MC", "Btn_MR", "Btn_MPlus", "Btn_MMinus", "Btn_MS" };
    }

    public Calc()
    {
        InitializeComponent();
        _calcStack.Push(new CalcArg(0));

        Calc_Display.Text = CalcStackGetLaslAgrument().Value.ToString();
        Cacl_Stack.Text = _calcStack.AsStr();
        Calc_Memory.Text = _memory.ToString();
    }

    private void OnCalcButtonClick(object sender, EventArgs e)
    {
        Cacl_Stack.Text = _calcStack.AsStr();

        Button clickedButton = sender as Button;
        if(clickedButton == null) return;

        string buttonTag = TagOf(clickedButton);

        switch(GetButtonType(buttonTag))
        {
            case ButtonType.Digits:
                Treat_DigitButton(buttonTag);
                break;
            case ButtonType.Operation:
                Treat_OperationButton(buttonTag);
                break;
            case ButtonType.Clear:
                Treat_ClearButton(buttonTag);
                break;
            case ButtonType.Memory:
                Treat_MemoryButton(buttonTag);
                break;
            default:
                MessageBox.Show("Unknown button type", "Error!");
                break;
        }

        Calc_Display.Text = CalcStackGetLaslAgrument().Value.ToString();
        Calc_Display.Text += buttonTag == "Btn_Dot" ? _digitBtns["Btn_Dot"] : string.Empty;

        Cacl_Stack.Text = _calcStack.AsStr();
        Calc_Memory.Text = _memory.ToString();
    }

    private void Treat_DigitButton(string buttonTag)
    {
        string updatedValueStr = Calc_Display.Text + _digitBtns[buttonTag];

        if(!double.TryParse(updatedValueStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double newValue)) return;

        CalcArg arg = CalcStackGetLaslAgrument();

        arg.Value = newValue;
    }

    private void Treat_OperationButton(string buttonTag)
    {
        CalcOp operation = new CalcOp(_opButtons[buttonTag]);
        if(operation.ArgsCount == 1)
        {
            CalcArg arg = CalcStackGetLaslAgrument();
            switch(operation.Kind)
            {
                case ECalcOperation.Percent:
                    Mark2ndOperandAsPercent();
                    break;
                case ECalcOperation.Fraction:
                    arg.Value = 1.0 / arg.Value;
                    break;
                case ECalcOperation.Sqrt:
                    arg.Value = Math.Sqrt(arg.Value);
                    break;
                case ECalcOperation.Inverse:
                    arg.Value *= -1;
                    break;
                case ECalcOperation.Equals:
                    Calculate();
                    break;
            }
        }
        if(operation.ArgsCount == 2)
        {
            CalcArg arg = new CalcArg(0);
            _calcStack.Push(arg);
            _calcStack.Push(operation);
        }
    }

    private void Treat_ClearButton(string buttonTag)
    {
        CalcArg arg = CalcStackGetLaslAgrument();
        switch(buttonTag)
        {
            case "Btn_CE":
                arg.Value = 0;
                break;
            case "Btn_Clear":
                _calcStack.Clear();
                _calcStack.Push(new CalcArg(0));
                break;
            case "Btn_Delete":
                StringBuilder sb = new StringBuilder(Calc_Display.Text);
                string updatedValueStr = sb.Remove(sb.Length - 1, 1).ToString();
                arg.Value = double.TryParse(updatedValueStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double newValue) ? newValue : 0;
                break;
        }
    }

    private void Treat_MemoryButton(string buttonTag)
    {
        switch(buttonTag)
        {
            case "Btn_MS": // Memory save
                _memory = CalcStackGetLaslAgrument().Value;
                break;
            case "Btn_MC": // Memory clear
                _memory = null;
                break;
        }

        if(_memory != null)
            switch(buttonTag)
            {
                case "Btn_MR": // Memory read
                    CalcStackGetLaslAgrument().Value = (double)_memory;
                    break;
                case "Btn_MPlus": // Memory plus
                    _memory += CalcStackGetLaslAgrument().Value;
                    break;
                case "Btn_MMinus": // Memory minus
                    _memory -= CalcStackGetLaslAgrument().Value;
                    break;
            }

        Btn_MC.Enabled = Btn_MR.Enabled = _memory != null;
    }

    private bool CalcStackGetBinaryOperationItems(out CalcOp baseBinaryOperation, out CalcArg argument2, out CalcArg argument1)
    {
        baseBinaryOperation = null;
        argument2 = null;
        argument1 = null;

        // Calc stack should contains three items: baseBinararyOperation, arg2, arg1
        if(_calcStack.Count != 3) return false;

        Stack<CalcItem> _calcStackCopy = new Stack<CalcItem>(_calcStack.Reverse());

        // Calc stack first item is base binary operation
        baseBinaryOperation = _calcStackCopy.Pop() as CalcOp;
        if(baseBinaryOperation == null || baseBinaryOperation.ArgsCount != 2) return false;

        // Calc stack second item is argument (of base binary operation)
        argument2 = _calcStackCopy.Pop() as CalcArg;
        if(argument2 == null) return false;

        // Calc stack first item is argument (of base binary operation)
        argument1 = _calcStackCopy.Pop() as CalcArg;
        if(argument1 == null) return false;

        return true;
    }

    private void Calculate()
    {
        if(!CalcStackGetBinaryOperationItems(out CalcOp baseBinaryOperation, out CalcArg argument2, out CalcArg argument1))
            return;

        double arg1Value = argument1.Value;
        double arg2Value = Arg2Value(argument1, argument2);

        CalcArg result;

        switch(baseBinaryOperation.Kind)
        {
            case ECalcOperation.Plus:
                result = new CalcArg(arg1Value + arg2Value);
                break;
            case ECalcOperation.Minus:
                result = new CalcArg(arg1Value - arg2Value);
                break;
            case ECalcOperation.Multiply:
                result = new CalcArg(arg1Value * arg2Value);
                break;
            case ECalcOperation.Divide:
                try
                {
                    result = new CalcArg(arg1Value / arg2Value);
                }
                catch(DivideByZeroException ex)
                {
                    MessageBox.Show("Division by zero not allowed!", "ERROR");
                    result = new CalcArg(0.00);
                }
                break;
            default: throw new InvalidOperationException("Unknown binary operation");
        }

        _calcStack.Clear();
        _calcStack.Push(result);
    }

    private double Arg2Value(CalcArg argument1, CalcArg argument2)
    {
        CalcAgrInPercents arg2inPercent = argument2 as CalcAgrInPercents;
        return arg2inPercent == null ? argument2.Value : (arg2inPercent.Value * argument1.Value / 100.0);
    }

    private void Mark2ndOperandAsPercent()
    {
        if(!CalcStackGetBinaryOperationItems(out CalcOp baseBinaryOperation, out CalcArg argument2, out CalcArg argument1))
            return;

        _calcStack.Clear();
        _calcStack.Push(argument1);
        _calcStack.Push(new CalcAgrInPercents(argument2.Value));
        _calcStack.Push(baseBinaryOperation);
    }

    private CalcArg CalcStackGetLaslAgrument()
    {
        Stack<CalcItem> _calcStackCopy = new Stack<CalcItem>(_calcStack.Reverse());
        CalcArg arg = null;

        while(_calcStackCopy.Count > 0 && arg == null)
            arg = _calcStackCopy.Pop() as CalcArg;

        return arg ?? throw new InvalidOperationException("Calc stack not contains any calculation argument");
    }

    private string TagOf(Button clickedButton) => clickedButton.Tag as string;

    private ButtonType GetButtonType(string buttonTag)
    {
        if(_digitBtns.ContainsKey(buttonTag)) return ButtonType.Digits;
        if(_opButtons.ContainsKey(buttonTag)) return ButtonType.Operation;
        if(_tags_clearButtons.Contains(buttonTag)) return ButtonType.Clear;
        if(_tags_memoryButtons.Contains(buttonTag)) return ButtonType.Memory;
        return ButtonType.UnknownType;
    }

    private void Calc_Load(object sender, EventArgs e) { }
}