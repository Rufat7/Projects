#include <iostream>
#include <string>
#include <vector>
#include <regex>
#include <map>
#include <fstream>

using namespace std;

class Person {
private:
    string name;
    string surname;

public:
    Person(const string& name, const string& surname) {
        this->name = name;
        this->surname = surname;
    }

    string getName() const {
        return name;
    }

    string getSurname() const {
        return surname;
    }
};

class Cards {
private:
    Person* card_owner{};
    string cardNumber;
    string cvc;
    double balance{};
    string type;

public:
    Cards() = default;
    Cards(Person* card_owner, const string& cardNumber, const string& cvc, double balance, string type) {
        this->card_owner = card_owner;
        this->cardNumber = cardNumber;
        this->cvc = cvc;
        this->balance = balance;
        this->type = type;
    }

    Person* getCardOwner() const {
        return card_owner;
    }

    string getCardNumber() const {
        return cardNumber;
    }

    string getCVC() const {
        return cvc;
    }

    double getBalance() const {
        return balance;
    }

    string getType() const {
        return type;
    }

    bool isCardNumberValid() const {

        regex pattern("\\d{16}");
        return regex_match(cardNumber, pattern);
    }

    bool isCVCValid() const {

        regex pattern("\\d{3}");
        return regex_match(cvc, pattern);
    }

    void deposit(double amount) {
        balance += amount;
    }

    bool withdraw(double amount) {
        if (amount <= balance) {
            balance -= amount;
            return true;
        }
        return false;
    }
};

class Expense {
public:
    string category;
    double amount;
    string date;

    Expense(string category, double amount, string date) : category(category), amount(amount), date(date) {}
};

class PersonalFinanceManager {
private:
    vector<double> wallets;
    vector<double> cards;
    vector<Expense> expenses;

public:
    void addWallet(double amount) {
        wallets.push_back(amount);
    }

    void addCard(double amount) {
        cards.push_back(amount);
    }

    void addExpense(string category, double amount, string date) {
        expenses.push_back(Expense(category, amount, date));
    }

    void generateReportByDay(string date) {
        double totalExpenses = 0;

        for (const Expense& expense : expenses) {
            if (expense.date == date) {
                cout << "Категория: " << expense.category << ", Количество: " << expense.amount << endl;
                totalExpenses += expense.amount;
            }
        }

        cout << "Общие Затраты: " << totalExpenses << endl;
    }

    void generateReportByWeek(string startDate, string endDate) {
        double totalExpenses = 0;

        for (const Expense& expense : expenses) {
            if (expense.date >= startDate && expense.date <= endDate) {
                cout << "Категория: " << expense.category << ", Количество: " << expense.amount << endl;
                totalExpenses += expense.amount;
            }
        }

        cout << "Общие Затраты: " << totalExpenses << endl;
    }

    void generateReportByMonth(string month, string year) {
        double totalExpenses = 0;

        for (const Expense& expense : expenses) {
            if (expense.date.substr(3, 7) == month + "-" + year) {
                cout << "Категория: " << expense.category << ", Количество: " << expense.amount << endl;
                totalExpenses += expense.amount;
            }
        }

        cout << "Общие Затраты: " << totalExpenses << endl;
    }

    void generateTopExpensesByWeek(string startDate, string endDate) {
        map<string, double> categoryExpenses;

        for (const Expense& expense : expenses) {
            if (expense.date >= startDate && expense.date <= endDate) {
                categoryExpenses[expense.category] += expense.amount;
            }
        }
        cout << "Топ 3 расходов по неделям:" << endl;
        int count = 0;
        for (const auto& pair : categoryExpenses) {
            if (count >= 3) {
                break;
            }
            cout << "Категория: " << pair.first << ", Количество: " << pair.second << endl;
            count++;
        }
    }

    void generateTopExpensesByMonth(string month, string year) {
        map<string, double> categoryExpenses;

        for (const Expense& expense : expenses) {
            if (expense.date.substr(3, 7) == month + "-" + year) {
                categoryExpenses[expense.category] += expense.amount;
            }
        }

        cout << "Топ 3 расходов по месяцам :" << endl;
        int count = 0;
        for (const auto& pair : categoryExpenses) {
            if (count >= 3) {
                break;
            }
            cout << "Категория: " << pair.first << ", Количество: " << pair.second << endl;
            count++;
        }
    }

    void saveReportsToFile(string filename) {
        ofstream file(filename);

        if (!file.is_open()) {
            cout << "Не удалось открыть файл." << endl;
            return;
        }

        file << "Затраты:" << endl;
        for (const Expense& expense : expenses) {
            file << "Категория: " << expense.category << ", Количество: " << expense.amount << ", Дата: " << expense.date << endl;
        }

        file.close();
    }
};


int main() {
    setlocale(LC_ALL, "Rus");
    PersonalFinanceManager manager;


    manager.addWallet(1000);
    manager.addCard(5000);


    manager.addWallet(500);
    manager.addCard(1000);


    manager.addExpense("Food", 50, "2023-01-01");
    manager.addExpense("Transportation", 20, "2023-01-01");
    manager.addExpense("Shopping", 100, "2023-01-02");
    manager.addExpense("Food", 30, "2023-01-03");

    manager.generateReportByDay("2023-01-01");
    manager.generateReportByWeek("2023-01-01", "2023-01-07");
    manager.generateReportByMonth("01", "2023");

    manager.generateTopExpensesByWeek("2023-01-01", "2023-01-07");
    manager.generateTopExpensesByMonth("01", "2023");


    manager.saveReportsToFile("finance_reports.txt");


    return 0;
}
