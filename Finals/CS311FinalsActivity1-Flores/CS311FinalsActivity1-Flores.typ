#align(center, text("Finals Activity 1", 27pt))
#grid(
  columns: (1fr, 1fr),
  align(left)[
    Marlon Alen I Flores\
    BSCS - 3A
  ],
  align(right)[
    CS311\
    #datetime.today().display()
  ],
)

#outline(depth: 1)

#show raw: set text(font: "JetBrainsMono NF")
#show link: set text(fill: blue)
#import "@preview/codly:1.3.0": *
#import "@preview/codly-languages:0.1.1": *
#import "@preview/cetz:0.4.2"
#show: codly-init.with()
#codly(languages: codly-languages, stroke: 0.3pt + black)
#show raw.where(block: true, lang: "console"): it => local(
  header: text(fill: black, font: "New Computer Modern", [*Output*]),
  header-cell-args: (fill: luma(240)),
  number-format: none,
  zebra-fill: none,
  display-icon: false,
  display-name: false,
  breakable: false,
  fill: rgb("#202032"),
  {
    show regex("^\$.*"): text.with(fill: rgb("#a6e3a1"))
    text(fill: white, it)
  },
)

= Screen Recording
#link("https://drive.google.com/file/d/1-kCQODRja99qOFla8jTgLyKT6rw4HjRL/view?usp=sharing")

= Scenarios
== Create 4 new tickets (2 hardware, 1 software, and 1 connection).
#image("./screenshots/scene1-1")
#image("./screenshots/scene1-2")
#image("./screenshots/scene1-3")
#image("./screenshots/scene1-4")
== Highlights the tickets you have created on instruction 1.
#image("./screenshots/scene2")
== Search all tickets with hardware problem.
#image("./screenshots/scene3")
== Update the first ticket you have created into connection.
#image("./screenshots/scene4-1")
#image("./screenshots/scene4-2")
#image("./screenshots/scene4-3")
#image("./screenshots/scene4-4")
== Search the first ticket you have created.
#image("./screenshots/scene5")
== Delete the first and last ticket you have created.
#image("./screenshots/scene6-1")
#image("./screenshots/scene6-2")
== Show the remaining tickets.
#image("./screenshots/scene7")
== Show the logs table.
#image("./screenshots/scene8")
= Code
Full code is available at
#link("https://github.com/Alen-Flores/CS311-2026-FLORES/tree/master/Finals/CS311FinalsActivity1-Flores")

#let code(file) = [
  === #file
  #raw(block: true, lang: "C#", read(file))
]

== Models
#code("./Models/Ticket.cs")

== Services
#code("./Services/TicketService.cs")

== ViewModel
#code("./ViewModels/Tickets/TicketsViewModel.cs")
#code("./ViewModels/Tickets/AddTicketViewModel.cs")
#code("./ViewModels/Tickets/UpdateTicketViewModel.cs")
