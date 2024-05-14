using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace HW17_WPF_Calc;


public partial class MainWindow : Window
{
    enum ButtonType
    {
        Digits,
        Operation,
        Clear,
        Memory,
        UnknownType
    }


    public MainWindow()
    {
        InitializeComponent();

        _calculationStack.Push(new CalcArg(0));

        Calc_Display.Text = CalculationStackGetLastArgument().Value.ToString();
        Calc_Stack.Text = _calculationStack.AsStr();
        Calc_Memory.Text = _memory.ToString();
    }

    Stack<CalcItem> _calculationStack = new ();

    private double? _memory = null;

    private static readonly Dictionary<string, string> _digitButtons;

    private static readonly Dictionary<string, ECalcOperation> _operationButtons;

    private static readonly HashSet<string> _clearButtons;

    private static readonly HashSet<string> _memoryButtons;

    static MainWindow()
    {
        _digitButtons = new()
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

        _operationButtons = new ()
            {
                { "Btn_Invert", ECalcOperation.Inverse },
                { "Btn_Sqrt", ECalcOperation.Sqrt },
                { "Btn_Divide", ECalcOperation.Divide },
                { "Btn_Percent", ECalcOperation.Percent },
                { "Btn_Multiply", ECalcOperation.Multiply },
                { "Btn_Fraction", ECalcOperation.Fraction },
                { "Btn_Minus", ECalcOperation.Minus },
                { "Btn_Plus", ECalcOperation.Plus },
                { "Btn_Equal", ECalcOperation.Equals },
            };

        _clearButtons = ["Btn_CE", "Btn_CA", "Btn_Del"];

        _memoryButtons = ["Btn_MC", "Btn_MR", "Btn_MS", "Btn_MPlus", "Btn_MMinus"];
    }

    private void OnCalculatorButtonClick(object sender, RoutedEventArgs e)
    {
        Calc_Stack.Text = _calculationStack.AsStr();

        Button? clickedButton = sender as Button;
        if(clickedButton == null) return;

        string buttonName = clickedButton.Name;


        switch(GetButtonType(clickedButton))
        {
            case ButtonType.Digits:
                Treat_DigitButton(buttonName);
                break;
            case ButtonType.Operation:
                Treat_OperationButton(buttonName);
                break;
            case ButtonType.Clear:
                Treat_ClearButton(buttonName);
                break;
            case ButtonType.Memory:
                Treat_MemoryButton(buttonName);
                break;
            default:
                MessageBox.Show("Unknown button type", "Error!");
                break;
        }

        Calc_Display.Text = CalculationStackGetLastArgument().Value.ToString();
        Calc_Display.Text += buttonName == "Btn_Dot" ? _digitButtons["Btn_Dot"] : string.Empty;

        Calc_Stack.Text = _calculationStack.AsStr();
        Calc_Memory.Text = _memory.ToString();
    }

    private void Treat_DigitButton(string buttonTag)
    {
        string updatedValueStr = Calc_Display.Text + _digitButtons[buttonTag];

        if(!double.TryParse(updatedValueStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double newValue)) return;

        CalcArg arg = CalculationStackGetLastArgument();

        arg.Value = newValue;
    }

    private void Treat_OperationButton(string buttonTag)
    {
        CalcOp operation = new (_operationButtons[buttonTag]);
        if(operation.ArgsCount == 1)
        {
            CalcArg arg = CalculationStackGetLastArgument();
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
            CalcArg arg = new (0);
            _calculationStack.Push(arg);
            _calculationStack.Push(operation);
        }
    }

    private void Treat_ClearButton(string buttonTag)
    {
        CalcArg arg = CalculationStackGetLastArgument();
        switch(buttonTag)
        {
            case nameof(Btn_CE):
                arg.Value = 0;
                break;
            case nameof(Btn_CA):
                _calculationStack.Clear();
                _calculationStack.Push(new CalcArg(0));
                break;
            case nameof(Btn_Del):
                StringBuilder sb = new (Calc_Display.Text);
                string updatedValueStr = sb.Remove(sb.Length - 1, 1).ToString();
                arg.Value = double.TryParse(updatedValueStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double newValue) ? newValue : 0;
                break;
        }
    }

    private void Treat_MemoryButton(string buttonTag)
    {
        switch(buttonTag)
        {
            case nameof(Btn_MS): // Memory save
                _memory = CalculationStackGetLastArgument().Value;
                break;
            case nameof(Btn_MC): // Memory clear
                _memory = null;
                break;
        }

        if(_memory != null)
            switch(buttonTag)
            {
                case nameof(Btn_MR): // Memory read
                    CalculationStackGetLastArgument().Value = (double)_memory;
                    break;
                case nameof(Btn_MPlus): // Memory plus
                    _memory += CalculationStackGetLastArgument().Value;
                    break;
                case nameof(Btn_MMinus): // Memory minus
                    _memory -= CalculationStackGetLastArgument().Value;
                    break;
            }

        Btn_MC.IsEnabled = Btn_MR.IsEnabled = _memory != null;
    }

    private bool CalculationStackGetBinaryOperationItems(out CalcOp baseBinaryOperation, out CalcArg argument2, out CalcArg argument1)
    {
        baseBinaryOperation = null!;
        argument2 = null!;
        argument1 = null!;

        // Calculation stack should contains three items: baseBinaryOperation, arg2, arg1
        if(_calculationStack.Count != 3) return false;

        Stack<CalcItem> _calculationStackCopy = new (_calculationStack.Reverse());

        // Calculation stack first item is base binary operation
        baseBinaryOperation = (_calculationStackCopy.Pop() as CalcOp)!;
        if(baseBinaryOperation == null || baseBinaryOperation.ArgsCount != 2) return false;

        // Calculation stack second item is argument (of base binary operation)
        argument2 = (_calculationStackCopy.Pop() as CalcArg)!;
        if(argument2 == null) return false;

        // Calculation stack first item is argument (of base binary operation)
        argument1 = (_calculationStackCopy.Pop() as CalcArg)!;
        if(argument1 == null) return false;

        return true;
    }

    private void Calculate()
    {
        if(!CalculationStackGetBinaryOperationItems(out CalcOp baseBinaryOperation, out CalcArg argument2, out CalcArg argument1))
            return;

        double arg1Value = argument1.Value!;
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
                catch(DivideByZeroException)
                {
                    MessageBox.Show("Division by zero not allowed!", "ERROR");
                    result = new CalcArg(0.00);
                }
                break;
            default: throw new InvalidOperationException("Unknown binary operation");
        }

        _calculationStack.Clear();
        _calculationStack.Push(result);
    }

    private double Arg2Value(CalcArg argument1, CalcArg argument2)
    {
        CalcAgrInPercents? arg2inPercent = argument2 as CalcAgrInPercents;
        return arg2inPercent == null ? argument2.Value : (arg2inPercent.Value * argument1.Value / 100.0);
    }

    private void Mark2ndOperandAsPercent()
    {
        if(!CalculationStackGetBinaryOperationItems(out CalcOp baseBinaryOperation, out CalcArg argument2, out CalcArg argument1))
            return;

        _calculationStack.Clear();
        _calculationStack.Push(argument1);
        _calculationStack.Push(new CalcAgrInPercents(argument2.Value));
        _calculationStack.Push(baseBinaryOperation);
    }

    private CalcArg CalculationStackGetLastArgument()
    {
        Stack<CalcItem> _calculationStackCopy = new (_calculationStack.Reverse());
        CalcArg? arg = null!;

        while(_calculationStackCopy.Count > 0 && arg == null)
            arg = _calculationStackCopy.Pop() as CalcArg;

        return arg ?? throw new InvalidOperationException("Calculation stack not contains any calculation argument");
    }

    private ButtonType GetButtonType(Button clickedButton)
    {
        string buttonName = clickedButton.Name;

        if(_digitButtons.ContainsKey(buttonName)) return ButtonType.Digits;
        if(_operationButtons.ContainsKey(buttonName)) return ButtonType.Operation;
        if(_clearButtons.Contains(buttonName)) return ButtonType.Clear;
        if(_memoryButtons.Contains(buttonName)) return ButtonType.Memory;
        return ButtonType.UnknownType;
    }
}