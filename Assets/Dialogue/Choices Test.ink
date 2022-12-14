INCLUDE globals.ink


Hello, it is really cold out here.
-> main

=== main ===
Would you let me in?
    + [Yes]
        -> chosen("yes")
    + [No]
        -> chosen("no")
    + [Who are you?]
        -> introduction

=== chosen(answer) ===
You chose {answer}!
-> END

=== introduction ===
My name is {old_man}.
~ met_old_man = true
-> main

=== function setSpeakerName(name) ===
~ old_man = name