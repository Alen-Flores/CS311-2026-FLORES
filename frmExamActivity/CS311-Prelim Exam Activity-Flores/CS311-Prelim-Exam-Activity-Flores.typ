#align(center, text("Prelim Exam Activity", 27pt))
#grid(
  columns: (1fr, 1fr),
  align(left)[
    Marlon Alen I Flores\
    BSCS - 3A
  ],
  align(right)[
    CS311\
    2026-07-01
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
#raw(block: true, lang: "c#", read("./MainWindow.axaml.cs"))

= Validator
#raw(block: true, lang: "c#", read("./Validators.cs"))

= Scenarios
== 1. All Inputs are empty, select permanent, and click submit.
#image("./screenshots/scene-1")
== 2. Rate is 20 and hours worked is empty, select permanent, and click submit.
#image("./screenshots/scene-2")
== 3. Rate is empty and hours worked is 20, select permanent, and click submit.
#image("./screenshots/scene-3")
== 4. Rate and hours worked is 20, select permanent, and click submit.
#image("./screenshots/scene-4")
== 5. Rate is 755 and hours worked is 20, select permanent, and click submit.
#image("./screenshots/scene-5")
== 6. Rate is 20 and hours worked is 100, select permanent, and click submit.
#image("./screenshots/scene-6")
== 7. Rate is 755 and hours worked is 100, select contractual, and click submit.
#image("./screenshots/scene-7")
== 8. Rate is 755 and hours worked is 100, select contractual, select SSS, and click submit.
#image("./screenshots/scene-8")
== 9. Rate is 755 and hours worked is 100, select contractual, select SSS, select pagibig, and click submit.
#image("./screenshots/scene-9")
== 10. Rate is 755 and hours worked is 100, select contractual, select SSS, select, pagibig, select philhealth, and click submit.
#image("./screenshots/scene-10")
== 11. Rate is 755 and hours worked is 100, select permanent, and click submit.
#image("./screenshots/scene-11")
== 12. Rate is 755 and hours worked is 100, select permanent, select SSS, and click submit.
#image("./screenshots/scene-12")
== 13. Rate is 755 and hours worked is 100, select permanent, select SSS, select pagibig, and click submit.
#image("./screenshots/scene-13")
== 14. Rate is 755 and hours worked is 100, select permanent, select SSS, select, pagibig, select philhealth, and click submit.
#image("./screenshots/scene-14")

= Video
#link("https://drive.google.com/file/d/1DQiKDV6anC-tLUHXxwQsv4vTSNVI129w/view?usp=sharing")
