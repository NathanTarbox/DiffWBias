# DiffWBias
Prototype of a diff algorithm

The core of this code is the Diff algorithm starting from a basis of LCS.

I had need of an algorithm for finding typos within what should've been a predicted set of inputs.  The concept was a script of inputs that a student would enter.  If they matched the instructor's prescribed input, all is well.  However opportunities for correction and education emerge when the student diverges from the script however briefly.  The trouble is that the instructor needs to find those divergences.  

I found an LCS algorithm that worked as desired, but none of the diff implementations I found did what I wanted, so I discovered my own.

I've left the code a little rough-shod.  Primarily this is to show that I was able to leave it at alone once I got it working.  Maybe I'll go back and clean it now that I've got a record of it "finished yet unrefined".  I frequently argue that "good enough" isn't, but seeking perfection will guarantee the failure of any significant project.  This isn't the self-documenting code I work to put out.  There are poorly named functions, basically Un-named variables (but hey, it's some deep CS), and even an extraneous loop I think.  The winner this time isn't good code; I celebrate the creation of this for solving a big CS problem.

Looking at it again, as I write this as an overall comment. . . I want to clean this up in case anyone wants to make use of it.  Tonight though, I'll just place it out there, hit commit, another case of "better now than never".
