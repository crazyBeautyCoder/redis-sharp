using System;

class Test {
	static void Main ()
	{
		var r = new Redis ();
		r.Set ("foo", "bar");
		if (r.Keys.Length < 1)
			Console.WriteLine ("error: there should be at least one key");
		if (r.GetKeys ("f*").Length < 1)
			Console.WriteLine ("error: there should be at least one key");
		
		if (r.TypeOf ("foo") != Redis.KeyType.String)
			Console.WriteLine ("error: type is not string");
		r.Set ("bar", "foo");
		r ["one"] = "world";
		if (r.GetSet ("one", "newvalue") != "world")
			Console.WriteLine ("error: Getset failed");
		if (!r.Rename ("one", "two"))
			Console.WriteLine ("error: failed to rename");
		if (r.Rename ("one", "one"))
			Console.WriteLine ("error: should have sent an error on rename");
		r.Db = 10;
		r.Set ("foo", "diez");
		if (r.GetString ("foo") != "diez"){
			Console.WriteLine ("error: got {0}", r.GetString ("foo"));
		}
		if (!r.Remove ("foo"))
			Console.WriteLine ("error: Could not remove foo");
		r.Db = 0;
		if (r.GetString ("foo") != "bar")
			Console.WriteLine ("error, foo was not bar");
		if (!r.ContainsKey ("foo"))
			Console.WriteLine ("error, there is no foo");
		if (r.Remove ("foo", "bar") != 2)
			Console.WriteLine ("error: did not remove two keys");
		if (r.ContainsKey ("foo"))
			Console.WriteLine ("error, foo should be gone.");
		r.Save ();
		r.BackgroundSave ();
		Console.WriteLine ("Last save: {0}", r.LastSave);
		//r.Shutdown ();

		var info = r.GetInfo ();
		foreach (var k in info.Keys){
			Console.WriteLine ("{0} -> {1}", k, info [k]);
		}
	}
}