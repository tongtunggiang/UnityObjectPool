# An Object Pool design pattern implementation in Unity

## Introduction
Let us imagine a crazy scenario. You live in a forest, without convenient access to furniture shops, which means when you need a chair, you have to craft it by yourself. If you have to craft a new chair every single time you need to use it, it is very inconvenient. The Object Pool pattern's intention is to create a bunch of chairs when you just move in to the forest, so that when you need to use it you just need to take it out from your warehouse.

## Usage

### Scene setup:
* Create an empty object and attach Pool Manager script.
* For each prefab that you want to put into pool, create an empty object as a child of the object above and attach Pool script to it. Assign the prefab value and put an integer value so that the pool create a few instances for you.

For example, the ObjectPools object holds the Pool Manager scripts, while its three children have Pool script.
![](https://i1.wp.com/i.imgur.com/fXiWkmQ.png?resize=643%2C255&ssl=1 "")

### Usage in code:
Very simple, normally when you need a new instance of a prefab, you do this:
![](https://i.imgur.com/Mr0jBeF.png "")

... you just need to replace the Instantiate line to this:

![](https://i.imgur.com/ZAMntT0.png "")

I provided a few overloaded versions so that you can use the pattern naturally and conveniently. The same applied when deleting objects, you just need to replace `Destroy(gameObject)` to `gameObject.Despawn()`.

You can also implement two special events: `OnSpawn`, which is called when the object is spawned if you call `prefab.Spawn()` and its variations, and `OnPreDisable`, which is called when the object is destroyed with `object.Despawn()`:

![](https://i.imgur.com/CIeB90E.png "")

## Need explaination?
I wrote a blog post about this matters ealier, you can check it out here:
http://tongtunggiang.com/blog/2017/09/19/simple-object-pool-better-performance-games/

In case you have any confusion, feel free to contact me via social links that I provided in my website.
