## Codecool Old Auction Book
# Implementing classes with behaviour in C#. 

# In this project we are going to model an auction process.
# The items for sale are very valuable old books in various categories: Archeology, Astronomy, Chemistry, History, Medicine, and Physics. 

# There are four entities involved in the auction process.
# 1.	Book: a book has an id, a title, a topic, and a price.
# 2.	Bidder: a bidder places Bids on the books in order to win the auction. A Bidder has an id, and an initial capital. They also have a favourite topic, and two other topics which they are interested in.
# 3.	Bid: a bid is placed by the Bidder. It has an id, a reference to the Bidder and a bid price.
# 4.	Transaction: a transaction is created when a book is successfully sold to a bidder. It has an id and a timestamp, and has reference to the Bid and the Bidder objects.

## We approached this project by breaking it up into four steps.
## 1.	Create the Book, Bid, and Transaction model classes.
## 2.	Create Bidder model class.
# o	A bidder starts out with an initial capital. They will only bid on a book if its topic is their favourite, or if it's within the two other topics which they are also interested in.
# o	A bidder has a certain threshold amount which they are willing to spend on a single book. If it's their favourite topic, they are happy to spend 50% of their capital on it. If it's only among their 'interested in' topics, they will spend 25% on the book.
# o	Once a bidder has won a book, their capital should be decreased by the amount the book has been sold for. The above rules still apply, but now they have less capital to play with. Example: a bidder starts out with 1000. A book comes up with their favourite topic. The price is 300, and he is a single bidder, so he wons. Now he has 700 remaining capital, and the 50% and 25% rules should be applied to the new amount.
# o	A bidder will only bid on a book if it's price is below their threshold.
# o	When pricing a bid, the bidder looks at the difference of its threshold (the max amount it's willing to pay) and the price, divides the range in half, and adds the result to the current price to outbid the most recent bidder.
# o	Once started bidding, a bidder will continue to place bids until they win the book or the price gets too high for them.
## 3.	Create the neccessary functions in Program.cs that will help us generate randomized books and bidders, get random prices, get a random topic from the Topic enum, and any other functions that we might need.
## 4.	Implement the auction algorithm and its related functions in Program.cs.
# The auction process looks like this: 
# 1. First off, a book is put on sale.
# 2.	Then, We filter the pool of bidders to only contain those that are interested in the topic of the book and are able to pay its price - the others are not playing in this round.
# 3.	If no bidders, then proceed with the next book.
# 4.	Then, get a random bidder from the interested pool, and make it place a bid. The first bid should be equal to the book's price. After that, a bid is only valid if it's higher than the previous.
# 5.	Now, the price has been updated, so the bidders re-evaluate. We should run the process again from step two, until there's only one bidder left, who'll win the book.
# If a book is sold, a transaction should be created and put in a collection. At the end, the program should report how many books were sold in total.
# A text file with an example output has been included in the starter code.
# When writing the algorithm, We print every step & their results to the console.

