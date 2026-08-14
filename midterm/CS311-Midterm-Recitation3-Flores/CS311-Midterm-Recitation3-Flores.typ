#align(center, text("Recitation-3", 27pt))
#grid(
  columns: (1fr, 1fr),
  align(left)[
    Marlon Alen I Flores\
    BSCS - 3A
  ],
  align(right)[
    CS311\
    #datetime(year: 2026, month: 8, day: 14).display()
  ],
)

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

== Quick note:
#quote()[
  Apologies for the sudden change in code structure, trying to emulate the program structure of a windows forms program proved to be a confusing an error-prone experience. As such I proceeded to refactor the code to more closely resemble standard Avalonia Programs (the ui framework for linux that I'm currently using). Especially since I am also using Avalonia for the UI of our project, I'd also rather use this as a learning experience.
]

#link("https://avaloniaui.net/")

= Scenarios
== Login each usertype and showing the file maintenance menu
=== With Administrator
#image("./screenshots/scene-1")
=== With Technical
#image("./screenshots/scene-2")
=== With User
#image("./screenshots/scene-3")

== Adding a new account and showing it on the accounts form.
=== Add Account Form
#image("./screenshots/scene-4")
=== With Invalid Input
#image("./screenshots/scene-5")
=== Account Added
#image("./screenshots/scene-6")
=== Updated Table
#image("./screenshots/scene-7")

== Updating the newly added account and showing it on the accounts form.
=== Update Account Form
#image("./screenshots/scene-8")
=== Account Updated
#image("./screenshots/scene-9")
=== Updated Table
#image("./screenshots/scene-10")

== deleting the newly added account and showing the accounts form.
=== Delete Confirmation Prompt
#image("./screenshots/scene-11")
=== Updated Table
#image("./screenshots/scene-12")

== Show the logs table.
#image("./screenshots/scene-13")

= Screen Recording
#link("https://drive.google.com/file/d/1KLSJ7PAbBXDtMvp2vpzWhRsqq6C1pgkJ/view?usp=sharing")

= Code

#let code(file) = [
  === #file
  #raw(block: true, lang: "C#", read(file))
]

== Models
#code("./Models/Log.cs")
#code("./Models/User.cs")

== Services
#code("./Services/DatabaseService.cs")

== ViewModel
#code("./ViewModels/LoginViewModel.cs")
#code("./ViewModels/MainWindowViewModel.cs")
#code("./ViewModels/AccountsViewModel.cs")
#code("./ViewModels/AddAccountViewModel.cs")
#code("./ViewModels/UpdateAccountViewModel.cs")

