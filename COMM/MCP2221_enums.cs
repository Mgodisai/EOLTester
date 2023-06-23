namespace Alber.Eol.Hardware
{
    public enum GP0_Designation : byte
    {
        GPIO = 0,
        SSPND,
        LED_URx
    }

    public enum GP1_Designation : byte
    {
        GPIO = 0,
        ClockOutput,
        ADC1,
        LED_UTx,
        InterruptDetector
    }

    public enum GP2_Designation : byte
    {
        GPIO = 0,
        USBCFG,
        ADC2,
        DAC1
    }

    public enum GP3_Designation : byte
    {
        GPIO = 0,
        LED_I2C,
        ADC3,
        DAC2
    }

    public enum GPIO_Direction : byte
    {
        Output = 0,
        Input
    }

    public enum DACVRM
    {
        VDD = 0,
        mV1024,
        mV2048,
        mV4096
    }
}
