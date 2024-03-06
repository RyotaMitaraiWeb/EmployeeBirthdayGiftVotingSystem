# EmployeeBirthdayGiftVotingSystem

## How to run
### Docker
The easiest way to run this app is to simply run the provided ``docker-compose`` file, which will take care of lifting the server. The only things you need to do manually are to run the migrations and potentially
configure an HTTPS certificate. The Docker variant also provides you with a web interface for PGAdmin4 to manage the database with a GUI. You can access it on port 5433. Check the ``.env`` file for the default email and passwords

Note that you may need to temporarily provide data about the database locally to run the migrations. Refer below for the ``usersecrets`` structure.

### Manually
You can insert the following in ``usersecrets.json``:
```json
{
  "DB_PASSWORD": "A!strongpassword1234",
  "DB_USER": "postgres",
  "DB_NAME": "birthday_gifts",
  "DB_HOST": "postgres"
}
```

(change values if you want to)

get yourself a running instance of PostgreSQL and then run the migrations.

## Functionality
Users log in with already-created accounts and can initiate a vote for a gift for another employee's birthday. There are several restrictions regarding votings:
- Users cannot start votes for themselves nor can they participate in any manner in votes for their birthdays.
- Users cannot initialize a vote for another user that currently has a voting
- Once a vote is concluded, the employee cannot have another vote until the next year (for example, if a vote for Employee X is initiated in 2022, then Employee X cannot have another vote for a gift until 2023)

All times are calculated in UTC. All gifts that the users can vote for are seeded. If more gifts or changes are desired, those must be configured in ``Data/Seeders/GiftSeeders.cs``

## Accounts
This app does not allow registration. Users can only log with accounts that have been seeded beforehand. These are the following accounts and their passwords (the format is ``[username], [password] [first_name] [last_name]``:
- therealjohn, 123456 (John Doe)
- therealjane, 654321 (Jane Doe)
- lee, password (Lee Everett)
- Alakazam, abrakadabra (Henry Wilson)
- ryota1, abcde (Ryota Mitarai)
- texas, austin (Joel Miller)

Users log in only with usernames and passwords.
