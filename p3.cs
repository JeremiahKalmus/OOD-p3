// Author: Jeremiah Kalmus
// File: p3.cs
// Date: April 29th, 2019
// Version: 1.0

/*
 * This driver will test a heterogeneous collection of objects in an array of size 9. The array will hold 3 objects of each type 
 * (pwdCheck, excessC, & CompundC). The objects will be initialized followed by testing the default parameters of each constructor
 * to ensure that if a password of unsupported length is entered, or a character of unsupported ascii characters is entered, or if
 * no parameters are entered at all, that the appropriate default parameters will be set. This leaves the rest of the testing to more
 * conventional means assuming that the user will enter the correct number of parameters upon object construction. 
 * ----------------------------------------------------------------------------------------------------------------------------------
 * TESTS:
 * 
 * Test_Default_Constructor_Parameters():
 * 
 * This test will use the first objects created of each type (objects 0, 3, & 6) and test each twice. The first test will test 
 * for the user entering the correct number of parameters, however, they exceed the defined range of acceptable values stated in 
 * the class documentation. The second test will be the creation of the object with no entered parameters. In either case, the 
 * predetermined default values set in the class object will take effect.
 * 
 * General_Password_Checking:
 * 
 * This test is a general test for all objects that will check randomly created passwords of random lengths and see if they are accepted
 * or rejected. 
 * 
 * Test_Object_State:
 * 
 * In this test, 20 passwords will be entered, which is what I decided to be the maximum p value (minimum password length requirement)
 * of any object. Entering 20 passwords will ensure that we see at least one state change from every object. Since previous tests were
 * run on the same objects, all objects should already have counted one password that has entered so the first transition should be p-1.
 * 
 * Test_CompundC_K_Toggles:
 * 
 * This test is specific to the compundC child class so only object 6 is tested here. I decided to only test one object since if the
 * functionality works on one compundC object, then the same functionality is shared with all other instances of the compundC class
 * and the functionality will work for all other compundC objects. This function tests the functionality of compundC
 * where after k amount of state toggles, the object will no longer be able to toggle the state of the object. I overwrite a compundC
 * object 6 with a new one so that it is ensured to be in its default state. I enter no parameters so I know the p and k values that it 
 * will be set at. I enter 2*(p*k) passwords into object 6 so it is clear that after (p*k) password have been entered, the state doesn't 
 * change. The password entered is the same everytime and should always be accepted when the object is active.
 * 
 * Test_Both_ExcessC_States:
 * 
 * This test is specific to excessC in that it tests both the on and off state of the objects. This test is specific to objects 3 - 5.
 * The default state of the objects is 'off', which in the case of the parent, is actually 'on'. Therefore, in excessC's off state, it acts
 * just like pwdCheck if it were in its 'on' state. This is checked just like the General_Password_Checking function explained above. 
 * Next, the excessC objects will be given p amount of passwords to force them to change state to 'on'. After this, they are given more 
 * passwords where they are checked whether the pth character is a digit, if case is mixed, (there is at least one capital letter and one 
 * lower case letter in the password) and if a $ exists in the password. Inherently, since the pth character must be a digit, all 
 * passwords less than p in length are rejected. There is no other pwdCheck functionality that excessC checks for, just the checks stated 
 * above. Since the objects in their 'on' state have specific requirements that are not easily met, a custom made password is entered
 * for object 3 when it is 'on' to greatly improve the chances of there being at least one accepted password out of the three objects
 * tested.
 * 
 * Test_Non_ASCII_Passwords:
 * 
 * This final test will recreate the entire heterogeneous array to ensure that all the objects are 'on'. Next, the passwords
 * entered will contain all supported ASCII character except for the first character of the password. This will be a character that
 * isn't supported and in turn, all passwords in all objects should be rejected. The length is 20 characters to ensure it meets the
 * minimum length requirement so there is no ambiguity why the password was rejected.
 * 
 * ----------------------------------------------------------------------------------------------------------------------------------
 */

using System;

namespace p3
{
    class p3
    {
        const string ASTERISK = "********************";
        const string CUSTOM_EXCESSC_PWD = "Lk12345678901234567$";
        const uint TEST_OBJ_ARRAY_SIZE = 9;
        const uint COMPUND_TEST_OBJ = 6;
        const uint EXCESSC_TEST_OBJ = 3;
        const int MAX_PWD_LENGTH_REQUIRMENT = 20;
        const uint EVEN_MOD = 2;
        const uint DEFAULT_NUM_STATE_CHANGES = 4;
        const int RANDOM_PASSWORD_LENGTH = 30;
        const string POSSIBLE_ASCII = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
            "abcdefghijklmnopqrstuvwxyz0123456789!\"%&'()*+,-./:;<=>?@[]\\^_`|{}~ $#";
        const string NON_ASCII = "èÆ«öçä";
        static pwdCheck[] Initialize_pwd_check_object_array(Random rand)
        {
            Console.WriteLine(ASTERISK + " Initializing Heterogeneous Collection of Objects " + ASTERISK);
            Console.WriteLine();

            const int MAX_STATE_CHANGES = 10;

            pwdCheck[] initializer_array = new pwdCheck[TEST_OBJ_ARRAY_SIZE];
            uint random_password_length;
            int random_valid_ascii_index;
            uint max_state_changes;

            for (uint i = 0; i < TEST_OBJ_ARRAY_SIZE; i++)
            {
                random_password_length = (uint)rand.Next(0, RANDOM_PASSWORD_LENGTH);
                random_valid_ascii_index = rand.Next(0, POSSIBLE_ASCII.Length);
                max_state_changes = (uint)rand.Next(0, MAX_STATE_CHANGES);
                if (i < TEST_OBJ_ARRAY_SIZE/3)
                {
                    initializer_array[i] = new pwdCheck(random_password_length, POSSIBLE_ASCII[random_valid_ascii_index]);
                    Console.WriteLine("Object " + i + " is a pwdCheck object.");
                }
                else if ((TEST_OBJ_ARRAY_SIZE / 3) <= i && TEST_OBJ_ARRAY_SIZE - (TEST_OBJ_ARRAY_SIZE / 3) > i)
                {
                    initializer_array[i] = new excessC(random_password_length, POSSIBLE_ASCII[random_valid_ascii_index]);
                    Console.WriteLine("Object " + i + " is an excessC object.");
                }
                else
                {
                    initializer_array[i] = new compundC(max_state_changes, random_password_length, 
                                                       POSSIBLE_ASCII[random_valid_ascii_index]);
                    Console.WriteLine("Object " + i + " is a compundC object.");
                }
                Console.WriteLine("Object " + i + " took in a password of length: " + random_password_length);
                Console.WriteLine("After construction, object " + i + " has a password length requirement of: " + 
                                    initializer_array[i].Minimum_Password_Length);
                Console.WriteLine("Object " + i + " has a forbidden character: " + POSSIBLE_ASCII[random_valid_ascii_index]);
                Console.WriteLine();
            }
            return initializer_array;
        }
        static void Test_Default_Constructor_Parameters(Random rand, pwdCheck[] pwd_check_object_array)
        {
            Console.WriteLine(ASTERISK + " Testing the cases when the client enters a value outside " + ASTERISK);
            Console.WriteLine(ASTERISK + " of 4-20 characters and the no argument constructor " + ASTERISK);
            Console.WriteLine();

            const uint TEST_DEF_CONSTR_AMOUNT = 2;
            const int MAX_STATE_TOGGLE_LENGTH = 15;

            uint random_password_length;
            int random_valid_ascii_index;
            uint random_toggle_max_length;

            for (uint i = 0; i < TEST_DEF_CONSTR_AMOUNT; i++)
            {
                random_password_length = (uint)rand.Next(MAX_PWD_LENGTH_REQUIRMENT, MAX_PWD_LENGTH_REQUIRMENT * 2);
                random_valid_ascii_index = rand.Next(0, POSSIBLE_ASCII.Length);
                random_toggle_max_length = (uint)rand.Next(MAX_STATE_TOGGLE_LENGTH, MAX_STATE_TOGGLE_LENGTH * 2);

                if (i == 0)
                {
                    pwd_check_object_array[i] = new pwdCheck(random_password_length, POSSIBLE_ASCII[random_valid_ascii_index]);

                    Console.WriteLine("For exceeding the bounds of 4-20 characters\nThe length of the password" +
                                        " entered into object " + i + " is: " + random_password_length);
                    Console.WriteLine("Forbidden character entered into object " + i + " is: " +
                                        POSSIBLE_ASCII[random_valid_ascii_index]);
                    Console.WriteLine("The minimum length of the password of object " + i + " after construction is: " +
                                        pwd_check_object_array[i].Minimum_Password_Length);
                    Console.WriteLine();
                }
                else
                {
                    pwd_check_object_array[0] = new pwdCheck();

                    Console.WriteLine("No parameters were entered into object " + 0 + " constructor and the minimum length set " +
                                        "by default is: " + pwd_check_object_array[0].Minimum_Password_Length);
                    Console.WriteLine("Forbidden character by default in object " + 0 + " is a space");
                    Console.WriteLine();
                }
            }



            for (uint i = 0; i < TEST_DEF_CONSTR_AMOUNT; i++)
            {
                random_password_length = (uint)rand.Next(MAX_PWD_LENGTH_REQUIRMENT, MAX_PWD_LENGTH_REQUIRMENT * 2);
                random_valid_ascii_index = rand.Next(0, POSSIBLE_ASCII.Length);
                random_toggle_max_length = (uint)rand.Next(MAX_STATE_TOGGLE_LENGTH, MAX_STATE_TOGGLE_LENGTH * 2);

                if (i == 0)
                {
                    pwd_check_object_array[COMPUND_TEST_OBJ] = new compundC(random_toggle_max_length, random_password_length,
                                                                            POSSIBLE_ASCII[random_valid_ascii_index]);
                    compundC obj6 = (compundC)pwd_check_object_array[COMPUND_TEST_OBJ];
                    Console.WriteLine("For exceeding the bounds of 4-20 characters\nThe length of the password" +
                                        " entered into object " + COMPUND_TEST_OBJ + " is: " + random_password_length);
                    Console.WriteLine("Forbidden character entered into object " + COMPUND_TEST_OBJ + " is: " +
                                        POSSIBLE_ASCII[random_valid_ascii_index]);
                    Console.WriteLine("The object toggle amount entered is : " + random_toggle_max_length);
                    Console.WriteLine("The minimum length of the password of object " + COMPUND_TEST_OBJ + " after construction is: " +
                                        pwd_check_object_array[COMPUND_TEST_OBJ].Minimum_Password_Length);
                    Console.WriteLine("The amount the object can be toggled after construction is: " + obj6.State_Change_Limit);
                    Console.WriteLine();
                }
                else
                {
                    pwd_check_object_array[COMPUND_TEST_OBJ] = new compundC();
                    compundC obj6 = (compundC)pwd_check_object_array[COMPUND_TEST_OBJ];
                    Console.WriteLine("No parameters will be entered into compundC test object (obj # 3)");
                    Console.WriteLine("The minimum length of the password obj " + COMPUND_TEST_OBJ + " will accept is: " +
                                        pwd_check_object_array[COMPUND_TEST_OBJ].Minimum_Password_Length);
                    Console.WriteLine("Forbidden character by default in object " + COMPUND_TEST_OBJ + " is a space");
                    Console.WriteLine("The amount the object can be toggled after construction is: " + COMPUND_TEST_OBJ +
                                        " can have is: " + obj6.State_Change_Limit);
                    Console.WriteLine();
                }
            }
            for (uint i = 0; i < TEST_DEF_CONSTR_AMOUNT; i++)
            {
                random_password_length = (uint)rand.Next(MAX_PWD_LENGTH_REQUIRMENT, MAX_PWD_LENGTH_REQUIRMENT * 2);
                random_valid_ascii_index = rand.Next(0, POSSIBLE_ASCII.Length);
                random_toggle_max_length = (uint)rand.Next(MAX_STATE_TOGGLE_LENGTH, MAX_STATE_TOGGLE_LENGTH * 2);

                if (i == 0)
                {
                    Console.WriteLine("For exceeding the bounds of 4-20 characters\nThe length of the password" +
                    " entered into object " + EXCESSC_TEST_OBJ + " is: " + random_password_length);
                    Console.WriteLine("Forbidden character entered into object " + EXCESSC_TEST_OBJ + " by default is: " +
                                        POSSIBLE_ASCII[random_valid_ascii_index]);
                    Console.WriteLine("The minimum length of the password of object " + EXCESSC_TEST_OBJ + 
                                        " after construction is: " + pwd_check_object_array[EXCESSC_TEST_OBJ].Minimum_Password_Length);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("No parameters will be entered into excessC test object (obj # 6)");
                    Console.WriteLine("The minimum length of the password obj " + EXCESSC_TEST_OBJ + " will accept by default is: " +
                                        pwd_check_object_array[EXCESSC_TEST_OBJ].Minimum_Password_Length);
                    Console.WriteLine("Forbidden character by default in object " + EXCESSC_TEST_OBJ + " is a space");
                    Console.WriteLine();
                }
            }
            

            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }
        static void General_Password_Checking(Random rand, pwdCheck[] pwd_check_object_array)
        {
            Console.WriteLine(ASTERISK + " Entering and Checking Passwords " + ASTERISK);
            Console.WriteLine();

            uint random_password_length;
            int random_valid_index;
            string password;

            for (uint i = 0; i < TEST_OBJ_ARRAY_SIZE; i++)
            {
                password = "";
                random_password_length = (uint)rand.Next(0, RANDOM_PASSWORD_LENGTH);
                for (uint j = 0; j < random_password_length; j++)
                {
                    random_valid_index = rand.Next(0, POSSIBLE_ASCII.Length);
                    password += POSSIBLE_ASCII[random_valid_index];
                }

                Console.WriteLine("The random password generated for object " + i + " is: " + password);
                Console.WriteLine("The minimum length requirement for passwords in object " + i + " is: " +
                                    pwd_check_object_array[i].Minimum_Password_Length);


                if (pwd_check_object_array[i].Password_Check(password))
                {
                    Console.WriteLine("The entered password is acceptable");
                }
                else
                {
                    Console.WriteLine("The entered password does not meet the requirements");
                }

                Console.WriteLine("The actual length of the password entered in object " + i + " is: " +
                                    pwd_check_object_array[i].Password_Length);
                Console.WriteLine();
            }
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }
        static void Test_Object_State(Random rand, pwdCheck[] pwd_check_object_array)
        {
            Console.WriteLine(ASTERISK + " Testing the state change (on/off toggle) functionality of the objects "
                                + ASTERISK);
            Console.WriteLine();
            Console.WriteLine("We will enter 20 passwords to ensure that all objects change state since 20 is the\n" +
                                "largest minimum password length. All objects will have a password already checked\n" +
                                "incrementing its counter. All objects should have a password count of 1 when this test begins.");
            Console.WriteLine();

            int random_valid_index;
            string password = "";

            for (uint i = 0; i < MAX_PWD_LENGTH_REQUIRMENT; i++)
            {
                random_valid_index = rand.Next(0, POSSIBLE_ASCII.Length);
                password += POSSIBLE_ASCII[random_valid_index];
            }

            for (uint i = 0; i < TEST_OBJ_ARRAY_SIZE; i++)
            {
                Console.WriteLine("Minimum password length (state toggle after this many checks): " +
                                    pwd_check_object_array[i].Minimum_Password_Length);
                Console.WriteLine();

                for (uint j = 0; j < MAX_PWD_LENGTH_REQUIRMENT; j++)
                {
                    pwd_check_object_array[i].Password_Check(password);
                    Console.WriteLine("Password entered: " + password);
                    Console.WriteLine("Is object " + i + " currently active?: " + pwd_check_object_array[i].isActive);
                }
                Console.WriteLine();
            }
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }
        static void Test_CompundC_K_Toggles(pwdCheck[] pwd_check_object_array)
        {
            Console.WriteLine(ASTERISK + " Testing the CompundC objects to verify that they can only change state k times " 
                                + ASTERISK);
            Console.WriteLine();
            Console.WriteLine("I will enter passwords p*(k+1) times in order to confirm that it no longer changes state\n" +
                                "after p*k entries. I will also start with a freshly constructed object 6.");
            Console.WriteLine();

            string password = "random_password";
            pwd_check_object_array[COMPUND_TEST_OBJ] = new compundC();
            compundC obj6 = (compundC)pwd_check_object_array[COMPUND_TEST_OBJ];

            uint p_times_k = pwd_check_object_array[COMPUND_TEST_OBJ].Minimum_Password_Length * (obj6.State_Change_Limit);

            for (int i = 0; i < 2*p_times_k; i++)
            {
                Console.WriteLine("Password is: " + password);
                if (obj6.Password_Check(password))
                {
                    Console.WriteLine("The entered password is acceptable");
                }
                else
                {
                    Console.WriteLine("The entered password does not meet the requirements");
                }
                Console.WriteLine("Password was entered and the active status is: " + obj6.isActive);
                Console.WriteLine("Can object 6 still be toggled?: " + obj6.canToggle);
                Console.WriteLine("Amount of passwords that needs to be entered before lock out is: " + p_times_k);
                Console.WriteLine("Count for how many passwords have been entered: " + (i+1));
                Console.WriteLine("The forbidden character is a space");
                Console.WriteLine();
            }
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }
        static void Test_Both_ExcessC_States(Random rand, pwdCheck[] pwd_check_object_array)
        {
            Console.WriteLine(ASTERISK + " Specific test tailored to testing excessC's on and off functionality " + ASTERISK);
            Console.WriteLine();

            uint random_password_length;
            int random_valid_ascii_index;
            char forbidden_char;
            string password;

            Console.WriteLine("First, we will test excessC functions in their off state and they will have the \n" +
                     "same functionality as pwdCheck");
            Console.WriteLine();

            for (uint i = EXCESSC_TEST_OBJ; i < COMPUND_TEST_OBJ; i++)
            {
                random_password_length = (uint)rand.Next(0, RANDOM_PASSWORD_LENGTH);
                random_valid_ascii_index = rand.Next(0, POSSIBLE_ASCII.Length);
                password = "";
                pwd_check_object_array[i] = new excessC(random_password_length, POSSIBLE_ASCII[random_valid_ascii_index]);
                forbidden_char = POSSIBLE_ASCII[random_valid_ascii_index];

                for (uint j = 0; j < random_password_length; j++)
                {
                    random_valid_ascii_index = rand.Next(0, POSSIBLE_ASCII.Length);
                    password += POSSIBLE_ASCII[random_valid_ascii_index];
                }
                if (i == EXCESSC_TEST_OBJ)
                {
                    password = CUSTOM_EXCESSC_PWD;
                }

                Console.WriteLine("The random password generated for object " + i + " is: " + password);
                Console.WriteLine("The minimum length requirement for passwords in object " + i + " is: " +
                                    pwd_check_object_array[i].Minimum_Password_Length);

                if (pwd_check_object_array[i].Password_Check(password))
                {
                    Console.WriteLine("The entered password is acceptable");
                }
                else
                {
                    Console.WriteLine("The entered password does not meet the requirements");
                }

                Console.WriteLine("The actual length of the password entered in object " + i + " is: " +
                                    pwd_check_object_array[i].Password_Length);
                Console.WriteLine("Object " + i + " has a forbidden character: " + forbidden_char);
                Console.WriteLine("Is the object on?: " + pwd_check_object_array[i].isActive);
                Console.WriteLine();
            }

            Console.WriteLine("Second, we will test excessC functions in their on state which they are in by default");
            Console.WriteLine();

            Console.WriteLine("Entering passwords to force each excessC object to change state.");
            Console.WriteLine();

            for (uint i = 0; i < MAX_PWD_LENGTH_REQUIRMENT; i++)
            {
                random_password_length = (uint)rand.Next(0, RANDOM_PASSWORD_LENGTH);
                password = "";

                for (uint j = 0; j < random_password_length; j++)
                {
                    random_valid_ascii_index = rand.Next(0, POSSIBLE_ASCII.Length);
                    password += POSSIBLE_ASCII[random_valid_ascii_index];
                }

                for (uint j = EXCESSC_TEST_OBJ; j < COMPUND_TEST_OBJ; j++)
                {
                    if (i < pwd_check_object_array[j].Minimum_Password_Length + 1)
                    {
                        pwd_check_object_array[j].Password_Check(password);
                    }
                }
            }

            for (uint i = EXCESSC_TEST_OBJ; i < COMPUND_TEST_OBJ; i++)
            {
                random_password_length = (uint)rand.Next(0, RANDOM_PASSWORD_LENGTH);
                password = "";

                for (uint j = 0; j < random_password_length; j++)
                {
                    random_valid_ascii_index = rand.Next(0, POSSIBLE_ASCII.Length);
                    password += POSSIBLE_ASCII[random_valid_ascii_index];
                }
                if (i == EXCESSC_TEST_OBJ)
                {
                    password = CUSTOM_EXCESSC_PWD;
                }

                Console.WriteLine("The random password generated for object " + i + " is: " + password);
                Console.WriteLine("The minimum length requirement for passwords in object " + i + " is: " +
                                    pwd_check_object_array[i].Minimum_Password_Length);

                if (pwd_check_object_array[i].Password_Check(password))
                {
                    Console.WriteLine("The entered password is acceptable");
                }
                else
                {
                    Console.WriteLine("The entered password does not meet the requirements");
                }

                Console.WriteLine("The actual length of the password entered in object " + i + " is: " +
                                    pwd_check_object_array[i].Password_Length);
                Console.WriteLine("Is the object on?: " + pwd_check_object_array[i].isActive);
                Console.WriteLine();
            }
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();

        }
        static void Test_Non_ASCII_Passwords(Random rand, pwdCheck[] pwd_check_object_array)
        {
            Console.WriteLine(ASTERISK + " Testing all objects with non supported password characters " + ASTERISK);
            Console.WriteLine();
            Console.WriteLine("Creating new objects to ensure they are in their default state");
            Console.WriteLine();

            pwd_check_object_array = Initialize_pwd_check_object_array(rand);

            Console.WriteLine(ASTERISK + ASTERISK + ASTERISK);
            Console.WriteLine("I will create passwords of length 20 to ensure they fullfil the length requirement.\n" +
                                "All passwords will be composed of real ASCII character except for the first character\n" +
                                "of each password. All passwords should fail the test for each object.");
            Console.WriteLine(ASTERISK + ASTERISK + ASTERISK);
            Console.WriteLine();

            int random_valid_index;
            int random_invalid_index;
            string password;

            for (int i = 0; i < TEST_OBJ_ARRAY_SIZE; i++)
            {
                password = "";
                random_invalid_index = rand.Next(0, NON_ASCII.Length);
                for (uint j = 0; j < MAX_PWD_LENGTH_REQUIRMENT; j++)
                {
                    random_valid_index = rand.Next(0, POSSIBLE_ASCII.Length);
                    if (j == 0)
                    {
                        password += NON_ASCII[random_invalid_index];
                    }
                    else
                    {
                        password += POSSIBLE_ASCII[random_valid_index];
                    }
                }
                Console.WriteLine("The random password generated for object " + i + " is: " + password);
                Console.WriteLine("The minimum length requirement for passwords in object " + i + " is: " +
                                    pwd_check_object_array[i].Minimum_Password_Length);


                if (pwd_check_object_array[i].Password_Check(password))
                {
                    Console.WriteLine("The entered password is acceptable");
                }
                else
                {
                    Console.WriteLine("The entered password does not meet the requirements");
                }

                Console.WriteLine("The actual length of the password entered in object " + i + " is: " +
                                    pwd_check_object_array[i].Password_Length);
                Console.WriteLine();
            }

            Console.WriteLine("Press enter to end program...");
            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            Random rand = new Random();
            pwdCheck[] pwd_check_object_array = Initialize_pwd_check_object_array(rand);
            Test_Default_Constructor_Parameters(rand, pwd_check_object_array);
            General_Password_Checking(rand, pwd_check_object_array);
            Test_Object_State(rand, pwd_check_object_array);
            Test_CompundC_K_Toggles(pwd_check_object_array);
            Test_Both_ExcessC_States(rand, pwd_check_object_array);
            Test_Non_ASCII_Passwords(rand, pwd_check_object_array);
        }
    }
}
