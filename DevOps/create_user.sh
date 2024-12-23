# should access in elastic search container
 curl -u elastic:1234qwer -X POST "http://localhost:9200/_security/user/james" -H 'Content-Type: application/json' -d' {   "password" : "1234qwer", "roles" : [ "kibana_system" ], "full_name" : "James User",  "email" : "james@example.com"}'
