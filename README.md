# Test Parser API

I created this for a simple test for a job, in order to start to:

* Show I can work in C# even though I have never created any kind of .NET application
* Demonstrate the basic ability to set up a simple API
* Fulfill the other requirements of the test
* Gain a little Docker knowledge, as it is probably a part of your CI-CD

I chose to build a .NET Core application since it seems like the most flexible and appropriate for CI-CD and deployment to clients.  I use XUnit for testing because it was taking too much time getting NUnit to work with net core.

The result is a very simple API, that exposes the following endpoint: `api/employees/location/{input-string}`.  Though simple, it wouldn't be hard to build it into a full ORM based API.

Where {input-string} is of the following format  (including the paranthesis):
```
(id,created,employee(id,firstname,employeeType(id), lastname),location)
```


Here are the assumptions about the string:
* All ids are integers
* `employee(...)` is the actual string employee followed by paranthesis and its contents
* The same for `employeeType(...)`
* Created is a string representing a date of format `YYYY-MM-dd`
* Extra spaces before or after commas are okay (otherwise not okay)
* The string must start and end with paranthesis
* Location can be any string (there are no specifications for state / country / city, etc.)

I created an API solution as well as a testing solution, that both are built and run by the Dockerfile.

The docker build command (run from the command line with "`docker build -t dotnetapp-dev .`") does the following:
* Builds the test and api projects
* Runs the tests, displaying the passing output

The tests were designed to catch some edge cases I could think of, but I didn't implement a test suite that could catch every conceivable error, although I think my code should catch most of them, with the following notable exception:
* firstname cannot contain the string "employeeType(...)", although that shouldn't be an issue as I don't know anybody by that name

Running the container (with "`docker run -p {PORT_TO_EXPOSE}:80 --rm dotnetapp-dev`") allows additional usage / testing of the api via standard HTTP get requests to:

```http
http://localhost:{PORT_TO_EXPOSE}/api/employees/location/{input-string}
```

How I actually parsed the string was by creating appropriate entities for the different parts of the string (employee-location, employee, and employeeType).  The api returns JSON of the appropriate format.  For example, the following call:

```http
http://localhost:{PORT_TO_EXPOSE}/api/employees/location/(3,2018-03-21,employee(2,Robinson,employeeType(4),%20Crusoe),A nice beach in the Pacific)
```

Produces the following output:
```JSON
{
    "created": "2018-03-21T00:00:00",
    "employee": {
        "id": 2,
        "firstname": "Robinson",
        "employeeType": {
            "id": 4
        },
        "lastname": "Crusoe"
    },
    "id": 3,
    "location": "A nice beach in the Pacific"
}
```

Date is a full string representation of the date object, which could be changed back to the input version that came in (`YYYY-MM-dd`).

The API also returns 400 errors with an appropriate response header / message for incorrect input strings.