//Setup for reading user input in Node.js
// This is required to get input from the console, instead of on a prompt from a website frontend
const readline = require('readline');
const rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout
});

function ask(question) {
    return new Promise((resolve) => {
        rl.question(question, (answer) => {
            resolve(answer);
        });
    });
}

//Main logic of application
// make async so that you can await on input so that it doesnt continually loop
async function main() {
  let running = true;

  while (running !== false) {
    console.clear()
    menu();
    let userInput = await ask("Choose a menu option: ");
    let choice = getValidIntegerInput(userInput);

    switch (choice) {
      case 1:
        await calculateSteps();
        //Pause to read results
        console.log("Press any key to return to the menu...");
        await ask("");
        break;
      case 2:
        //Exit the program
        running = false;
        break;
      default:
        console.log("Invalid Option,Please try again");
        await ask(""); // Waits for user to press Enter
        break;
    }
  }
}


//Menu
function menu() {
  console.log("Welcome to the bay change Assistant, Select an Option");
  console.log("1. Calculate How much steps remain");
  console.log("2. Exit");
}

//Validate input to be an integer
function getValidIntegerInput(input) {
  let number;

  number = parseInt(input);

  if (!isNaN(number)) {
    //it was a number but is negative
    if (number < 0) {
      return -1;
    } else {
      //return valid number
      return number;
    }
  } else {
    console.log("Error: That is not a valid number. Please enter an integer.");
    return -1; // Return a failure value
  }
}

//Calculation and display steps
async function calculateSteps() {
  let totalSteps;
  let completedSteps;
  let input;

  // Loop to get validated Total Steps
  while (true) {
    input = await ask("Enter the total number of steps: ");

    //Validate - returns -1 if invalid, the number if valid
    totalSteps = getValidIntegerInput(input);

    // Check if valid number was entered
    if (totalSteps !== -1) {
      break; // Valid number, exit the loop
    }
  }

  //Loop to get validated Completed Steps
  while (true) {
    input = await ask("Enter the number of steps already completed: ");

    //Validate - returns -1 if invalid, the number if valid
    completedSteps = getValidIntegerInput(input);

    //Check for all error conditions
    if (completedSteps === -1) {
      // Input was not a valid number. Loop again.
    } else if (completedSteps > totalSteps) {
      console.log(
        `Error: Completed steps (${completedSteps}) cannot be greater than total steps (${totalSteps}).`
      );
      // Loop again
    } else {
      // Valid number and valid logic. Exit the loop.
      break;
    }
  }

  //Get Bay Name
  const bayName = await ask('Enter the current bay name (e.g., "Bay 3"): ');

  //Calculate and Display
  if (completedSteps === totalSteps) {
    console.log("Machine completed assembly — ready for next bay?");
  } else {
    const remainingSteps = totalSteps - completedSteps;
    const progressPercent = completedSteps / totalSteps;

    console.log(`Machine currently in ${bayName}`);
    console.log(`Steps completed: ${completedSteps} of ${totalSteps}`);
    console.log(`Remaining steps: ${remainingSteps}`);

    console.log(`Progress: ${progressPercent * 100}%`);
  }
}


// run program
main()
