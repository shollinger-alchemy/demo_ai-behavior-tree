# AI Behaviour Tree
This repository is for demonstration purposes only, and is not suitable for production.  The code is not guaranteed to run in Unity as-is.

## Overview
Behaviour Trees are a popular way to perform more complicated decision-making for an AI Agent than a state machine.  The demo in this repository shows a custom implementation of a Behaviour Tree and an example of a configured Tree for a patrolling enemy.

The general idea of a Behaviour Tree is that the root is evaluated, and a depth-first traversal of the tree is performed.  Each node visited returns a boolean - whether the previous node was 'successful'.  If it was successful, the traversal is stopped.  If it was not, then the traversal continues until there are no more nodes left.

This flexible system allows for nodes to perform any action as long as a boolean is returned.  For example, Conditional Nodes will check if a condition is true or false (ie. game state), Action Nodes will signal to a listener that an Action has been decided on, and Selector Nodes will only run one child that is selected from some arbitrary criteria.  Generally, Conditional Nodes preceed Action Nodes to create an if-then paradigm.  Nodes can also be nested to define complex and chained behaviours.

There are serveral references to Perceptors, which can be found in the [AI Agent Perception](https://github.com/shollinger-alchemy/demo_ai-agent-perception) demo.

## Technologies Used
* Unity AI
* Unity Events

## Concepts Shown
* A decoupled abstract framework for scalable solution building
* Behaviour Tree Decision-Making
* Event-Based Signaling
* Coroutine Usage
* Recursion
