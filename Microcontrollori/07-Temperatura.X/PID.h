double pid_controller(double setpoint, double process_variable, double kp, double ki, double kd, double control_min, double control_max) {
    static double previous_error = 0;
    static double integral = 0;
    double error = setpoint - process_variable;
    integral += error;
    double derivative = error - previous_error;
    double control = kp * error + ki * integral + kd * derivative;
    // anti-windup
    if (control < control_min) {
        integral -= error; // "unwind" the integral term
        control = control_min;
    }
    else if (control > control_max) {
        integral -= error; // "unwind" the integral term
        control = control_max;
    }
    previous_error = error;
    return control;
}