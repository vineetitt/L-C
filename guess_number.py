import random

def is_valid_guess(user_input):
    return user_input.isdigit() and 1 <= int(user_input) <= 100

def get_valid_guess():
    while True:
        user_input = input("Guess a number between 1 and 100:")
        if is_valid_guess(user_input):
            return int(user_input)
        print("I wont count this one Please enter a number between 1 to 100")

def provide_feedback(user_guess, target_number):
    if user_guess < target_number:
        return "Too low. Guess again"
    elif user_guess > target_number:
        return "Too High. Guess again"
    return None

def main():
    target_number = random.randint(1, 100)
    number_of_guesses = 0
    is_correct_guess = False

    user_guess = get_valid_guess()  

    while not is_correct_guess:
        number_of_guesses += 1

        feedback = provide_feedback(user_guess, target_number)
        if feedback:
            user_guess = int(input(feedback))
        else:
            print("You guessed it in", number_of_guesses, "guesses!")
            is_correct_guess = True

main()
