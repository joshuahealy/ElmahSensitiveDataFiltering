ElmahSensitiveDataFiltering
===========================

Package for sanitising sensitive form values in ELMAH logs for MVC 3 and 4

Install MVC4 Package Via Nuget: 
PM> Install-Package ElmahSensitiveDataFiltering.MVC4

Install MVC3 Package Via Nuget: 
PM> Install-Package ElmahSensitiveDataFiltering.MVC3

Why?
====

ELMAH is a fantastic logging tool and it grabs a whole lot of information about a request when it logs an error, including posted form data.  Most of the time this is okay, but there's some data like passwords and credit card numbers that you probably don't want stored in your logs as plain text!

The Elmah Sensitive Data Filtering package provides an easy way to sanitise such data, like so...

How?
====

Just place an attribute on your action with the names of the data you want sanitised, like so:

```
[HttpPost]
[ElmahSensitiveData("Password")]
public ActionResult Login(LoginModel model) { ... }

[HttpPost]
[ElmahSensitiveData("CreditCardNumber", "CreditCardExpiry")]
public ActionResult Checkout(CheckoutModel model) { ... }
```

This changes ELMAH's output (in the "AllXml" column if you're logging to a database) from something like this:
````
<error>
	....
	<form>
		<item name="Username">
			<value string="bob@example.com" />
		</item>
		<item name="Password">
			<value string="superSecretDontTellAnyone" />
		</item>
		...
	</form>
</error>
````

to this:
````
<error>
	....
	<form>
		<item name="Username">
			<value string="bob@example.com" />
		</item>
		<item name="Password">
			<value string="******" />
		</item>
		...
	</form>
</error>
````

The default replacement is ****** but this can be configured by adding an application setting in your web.config, like so:
````
<add key="ElmahSensitiveDataFiltering.SensitiveDataReplacementText" value="--//\\// VALUE SANITISED \\//\\--" />
````

License
=======
Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at

  http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
