using GreetingService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.Infrastructure.ApprovalService
{
    public static class AdaptiveCardTemplate
    {
        public static string ReturnJson(User user)
        {
            var jsonFile =
                 @$"{{{{
                    ""type"": ""AdaptiveCard"",
                    ""body"": [
                        {{
                            ""type"": ""ColumnSet"",
                            ""style"": ""emphasis"",
                            ""columns"": [
                                {{
                                    ""type"": ""Column"",
                                    ""items"": [
                                        {{
                                            ""type"": ""TextBlock"",
                                            ""text"": ""USER APPROVAL"",
                                            ""wrap"": true,
                                            ""size"": ""ExtraLarge"",
                                            ""weight"": ""Bolder"",
                                            ""color"": ""Attention""
                                        }}
                                    ],
                                    ""width"": ""stretch"",
                                    ""padding"": ""None""
                                }},
                                {{
                                    ""type"": ""Column"",
                                    ""items"": [
                                        {{
                                            ""type"": ""TextBlock"",
                                            ""horizontalAlignment"": ""Right"",
                                            ""color"": ""Accent"",
                                            ""text"": ""[#TS-093249-90](https://eur.delve.office.com/?u=acacf547-6bdb-41f8-b6b8-dac5cb97e49f&v=work)"",
                                            ""wrap"": true
                                        }}
                                    ],
                                    ""width"": ""stretch"",
                                    ""padding"": ""None""
                                }}
                            ],
                            ""padding"": ""Default"",
                            ""spacing"": ""None""
                        }},
                        {{
                            ""type"": ""Container"",
                            ""id"": ""7d00f965-40bb-9fc3-ff7b-a9b82a09ead4"",
                            ""padding"": ""Default"",
                            ""items"": [
                                {{
                                    ""type"": ""ColumnSet"",
                                    ""columns"": [
                                            {{
                                                ""type"": ""Column"",
                                                ""items"": [
                                                    {{
                                                        ""type"": ""Image"",
                                                        ""style"": ""Person"",
                                                        ""url"": ""https://i.vimeocdn.com/portrait/16052559_640x640"",
                                                        ""size"": ""Small"",
                                                        ""width"": ""100px"",
                                                        ""height"": ""100px""
                                                    }}
                                                ],
                                                ""width"": ""auto"",
                                                ""padding"": ""None""
                                            }},
                                            {{
                                                ""type"": ""Column"",
                                                ""items"": [
                                                    {{
                                                        ""type"": ""TextBlock"",
                                                        ""text"": ""{user.FirstName} {user.LastName}"",
                                                        ""wrap"": true,
                                                        ""weight"": ""Bolder"",
                                                        ""size"": ""Large""
                                                    }},
                                                    {{
                                                        ""type"": ""TextBlock"",
                                                        ""spacing"": ""None"",
                                                        ""color"": ""Light"",
                                                        ""text"": ""The King of Bad Requests"",
                                                        ""wrap"": true,
                                                        ""size"": ""Small""
                                                    }},
                                                    {{
                                                        ""type"": ""TextBlock"",
                                                        ""spacing"": ""None"",
                                                        ""color"": ""Light"",
                                                        ""text"": ""Stockholm"",
                                                        ""wrap"": true,
                                                        ""size"": ""Small""
                                                    }}
                                                ],
                                                ""width"": ""stretch"",
                                                ""padding"": ""None""
                                            }}
                                    ],
                                    ""spacing"": ""None"",
                                    ""padding"": ""None""
                                }}
                            ],
                            ""spacing"": ""None"",
                            ""separator"": true
                        }},
                        {{
                            ""type"": ""Container"",
                            ""items"": [
                                {{
                                    ""type"": ""TextBlock"",
                                    ""text"": ""*\""Please approve {user.FirstName} as a new user for the Greeting Service\""*"",
                                    ""wrap"": true,
                                    ""size"": ""Large""
                                }}
                            ],
                            ""padding"": {{
                                ""top"": ""None"",
                                ""bottom"": ""Default"",
                                ""left"": ""Default"",
                                ""right"": ""Default""
                            }},
                            ""spacing"": ""None""
                        }},
                        {{
                            ""type"": ""Container"",
                            ""id"": ""353b659f-b668-fac0-5b7f-5d2f1bdb46ac"",
                            ""padding"": ""Default"",
                            ""items"": [
                                {{
                                    ""type"": ""ActionSet"",
                                    ""actions"": [
                                        {{
                                            ""type"": ""Action.Http"",
                                            ""id"": ""accept"",
                                            ""title"": ""Accept"",
                                            ""method"": ""POST"",
                                            ""url"": ""https://towafunctionapp.azurewebsites.net/api/user/approve/{user.ApprovalCode}"",
                                            ""body"": ""{{ }}"",
                                            ""isPrimary"": true,
                                            ""style"": ""positive""
                                        }},
                                        {{
                                            ""type"": ""Action.ShowCard"",
                                            ""id"": ""e1487cbc-66b0-037e-cdc4-045fb7d8d0b8"",
                                            ""title"": ""Reject"",
                                            ""card"": {{
                                                ""type"": ""AdaptiveCard"",
                                                ""body"": [
                                                    {{
                                                        ""type"": ""Input.Text"",
                                                        ""id"": ""Comment"",
                                                        ""placeholder"": ""Add a comment for your rejection"",
                                                        ""isMultiline"": true
                                                    }},
                                                    {{
                                                        ""type"": ""ActionSet"",
                                                        ""id"": ""1e77f639-e5a8-320f-c6de-4291227db6b3"",
                                                        ""actions"": [
                                                            {{
                                                                ""type"": ""Action.Http"",
                                                                ""id"": ""1ca3a888-ebfb-1feb-064b-928960616e52"",
                                                                ""title"": ""Submit"",
                                                                ""method"": ""POST"",
                                                                ""url"": ""https://towafunctionapp.azurewebsites.net/api/user/reject/{user.ApprovalCode}"",
                                                                ""body"": ""{{comment: {{{{Comment.value}}}}}}""
                                                            }}
                                                        ]
                                                    }}
                                                ],
                                                ""$schema"": ""http://adaptivecards.io/schemas/adaptive-card.json"",
                                                ""fallbackText"": ""Unable to render the card"",
                                                ""padding"": ""None""
                                            }}
                                        }}
                                    ],
                                    ""horizontalAlignment"": ""Right""
                                }}
                            ],
                            ""spacing"": ""None""
                        }}
                    ],
                    ""$schema"": ""http://adaptivecards.io/schemas/adaptive-card.json"",
                    ""version"": ""1.0"",
                    ""padding"": ""None""
                }}}}";

            return jsonFile;
        }
    }
}
