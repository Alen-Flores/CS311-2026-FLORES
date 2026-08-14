#align(center, text("Recitation-1", 27pt))
#grid(
  columns: (1fr, 1fr),
  align(left)[
    Marlon Alen I Flores\
    BSCS - 3A
  ],
  align(right)[
    CS311\
    #datetime(year: 2026, month: 7, day: 29).display()
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


= Code

== Class1
#raw(block: true, lang: "cs", read("./Class1.cs"))

== Login
#raw(block: true, lang: "cs", read("./LoginWindow.axaml.cs"))

== MainForm
#raw(block: true, lang: "cs", read("./MainForm.axaml.cs"))

= Screenshots
#image("./screenshots/scene-1")
#image("./screenshots/scene-2")
#image("./screenshots/scene-3")
#image("./screenshots/scene-4")
#image("./screenshots/scene-5")

= Video
#link("https://drive.google.com/file/d/1CbdWmu2tz0mlgmBYdOtK081RRC2gIhPT/view?usp=sharing")
