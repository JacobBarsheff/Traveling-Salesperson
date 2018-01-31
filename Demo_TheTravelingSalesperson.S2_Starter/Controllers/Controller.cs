using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_TheTravelingSalesperson
{

    public class Controller
    {
        #region FIELDS

        private bool _usingApplication;
        private ConsoleView _consoleView;
        private Salesperson _salesperson;

        #endregion

        #region PROPERTIES


        #endregion

        #region CONSTRUCTORS

        public Controller()
        {
            InitializeController();

            //
            // instantiate a Salesperson object
            //
            _salesperson = new Salesperson();

            //
            // instantiate a ConsoleView object
            //
            _consoleView = new ConsoleView();

            //
            // begins running the application UI
            //
            ManageApplicationLoop();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// initialize the controller 
        /// </summary>
        private void InitializeController()
        {
            _usingApplication = true;
        }

        private void ManageApplicationLoop()
        {
            MenuOption userMenuChoice;

            _consoleView.DisplayWelcomeScreen();

            //
            // setup initial salesperson account
            //
            _salesperson = _consoleView.DisplaySetupAccount();

            //
            // application loop
            //
            while (_usingApplication)
            {

                //
                // get a menu choice from the ConsoleView object
                //
                userMenuChoice = _consoleView.DisplayGetUserMenuChoice();

                //
                // menu structure with correlating methods
                //
                switch (userMenuChoice)
                {
                    case MenuOption.None:
                        break;
                    case MenuOption.Travel:
                        Travel();
                        break;
                    case MenuOption.Buy:
                        Buy();
                        break;
                    case MenuOption.Sell:
                        Sell();
                        break;
                    case MenuOption.DisplayInventory:
                        DisplayInventory();
                        break;
                    case MenuOption.DisplayCities:
                        DisplayCities();
                        break;
                    case MenuOption.DisplayAccountInfo:
                        DisplayAccountInfo();
                        break;
                    case MenuOption.Exit:
                        _usingApplication = false;
                        break;
                    default:
                        break;
                }
            }

            _consoleView.DisplayClosingScreen();

            //
            // end the application
            //
            Environment.Exit(1);
        }

        private void Travel()
        {
            string nextCity = _consoleView.DisplayGetNextCity();
            //if nextCity is NOT blank, then add it to the list. 
            if (nextCity != "")
            {
                _salesperson.CitiesVisited.Add(nextCity);
            }
        }

        private void Buy()
        {
            int numberOfUnits = _consoleView.DisplayGetNumberOfUnitsToBuy(_salesperson.CurrentStock);
            _salesperson.CurrentStock.AddProducts(numberOfUnits);
        }


        private void Sell()
        {
            int numberOfUnits = _consoleView.DisplayGetNumberOfUnitsToSell(_salesperson.CurrentStock);
            _salesperson.CurrentStock.SubtractProducts(numberOfUnits);

            if (_salesperson.CurrentStock.OnBackorder)
            {
                _consoleView.DisplayBackorderNotification(_salesperson.CurrentStock, numberOfUnits);
            }
        }

        private void DisplayInventory()
        {
            _consoleView.DisplayInventory(_salesperson.CurrentStock);
        }


        private void DisplayCities()
        {
            _consoleView.DisplayCitiesTraveled(_salesperson);
        }

        private void DisplayAccountInfo()
        {
            _consoleView.DisplayAccountInfo(_salesperson);
        }

        #endregion
    }
}