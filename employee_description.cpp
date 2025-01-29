class Employee {
    int id;
    string name;
    string department;
    bool working;

public:
    void terminateEmployee();
    bool isWorking();
};

class EmployeeDatabase {
public:
    void saveEmployeeToDatabase(const Employee& employee);
};

class EmployeeReport {
public:
    void printEmployeeDetailReportXML(const Employee& employee);
    void printEmployeeDetailReportCSV(const Employee& employee);
};

